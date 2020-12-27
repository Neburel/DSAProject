import { AfterContentInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GenericMatTableComponent } from 'src/app/components/generic-mat-table/generic-mat-table.component';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { TalentService } from 'src/app/services/dsa/talent.service';
import { GenericDataTableColumn, Language } from 'src/app/types';
import { AddDbaMatTableRecID } from 'src/app/util/utilGenericDataTable';

const CURRENTPROBE = "CurrentProbe";

@Component({
    selector: 'app-language-view',
    templateUrl: './language-view.component.html',
    styleUrls: ['./language-view.component.scss']
})
/** languageView component*/
export class LanguageViewComponent implements OnInit, AfterContentInit {
    @ViewChild(GenericMatTableComponent, { static: true }) dataTableMain: GenericMatTableComponent;
    public dataSourceMain = new MatTableDataSource();
    public columnsdefMain: GenericDataTableColumn[];

    /** languageView ctor */
    constructor(private charakterService: CharakterService, public talentService: TalentService) { }

    ngOnInit(): void {
        this.createTable();
    }
    ngAfterContentInit(): void {
        this.LoadDataMain();
    }

    private createTable() {
        var smallWidth = 60;
        var tawWidth = 100;

        this.columnsdefMain = [
            { id: 'Sprache', label: 'Sprache', hideOrder: 0 },
            {
                id: 'Mother', label: 'M.', hideOrder: 0, datatype: "checkbox", width: smallWidth, nullCheckID: 'Sprache', clickEvent: (talent: Language) => {
                    talent.Mother = !talent.Mother;
                    this.SetTalent(talent);
                }
            },
            { id: 'KomplexSprache', label: 'K.', hideOrder: 0, width: smallWidth },
            {
                id: 'TawSprache', label: 'TAW', nullCheckID: 'Sprache', hideOrder: 0, datatype: 'taw', width: tawWidth, clickEvent: (talent: Language, input: number) => {
                    talent.TawSprache = talent.TawSprache + input;
                    this.SetTalent(talent);
                }
            },
            { id: 'ProbeSprache', label: 'Probe', hideOrder: 0, width: tawWidth, },
            { id: 'Schrift', label: 'Schrift', hideOrder: 0 },
            { id: 'KomplexSchrift', label: 'K.', hideOrder: 0, width: smallWidth },
            {
                id: 'TawSchrift', label: 'TAW', nullCheckID: 'Schrift', hideOrder: 0, datatype: 'taw', width: tawWidth, clickEvent: (talent: Language, input: number) => {
                    talent.TawSchrift = talent.TawSchrift + input;
                    this.SetTalent(talent);
                }
            },
            { id: 'ProbeSchrift', label: 'Probe', hideOrder: 0, width: tawWidth, },
            {
                id: CURRENTPROBE, label: '', hideOrder: 0, width: 80, noexport: true
            },
        ]
    }

    public LoadDataMain() {
        this.dataTableMain.isLoading = true;
        this.talentService.GetLanguageList(this.charakterService.CurrentCharakter).then(dataList => {
            this.dataSourceMain.data = AddDbaMatTableRecID<Language>(dataList, (element) => {
                return element;
            });
            this.dataTableMain.isLoading = false;
        })
    }
    public SetTalent(talent: Language) {
        this.talentService.SetLanguage(this.charakterService.CurrentCharakter, talent).then(result => {
            this.LoadDataMain();
        })
    }
    public probeInput(value: number) {
        this.dataTableMain.dataSource.data.forEach((element: Language) => {
            console.log(element.IsTitle);

            if (!element.IsTitle) {
                var probeWriting = Number(element.ProbeSchrift) - value;
                var probeSpeaking = Number(element.ProbeSprache) - value;

                if (element.Sprache != null && element.Schrift != null) {
                    element[CURRENTPROBE] = probeWriting + '/' + probeSpeaking
                }
                else if (element.Sprache != null) {
                    element[CURRENTPROBE] = probeSpeaking
                }
                else if (element.Schrift != null) {
                    element[CURRENTPROBE] = probeWriting
                }
            }
        });
    }
}