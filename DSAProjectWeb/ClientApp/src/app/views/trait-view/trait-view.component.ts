import { Attribute, Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { unwatchFile } from 'fs';
import { CreateTraitDialogComponent } from 'src/app/dialogs/create-trait-dialog/create-trait-dialog.component';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { TraitService } from 'src/app/services/dsa/trait.service';
import { DBADataTableSelect, GenericDataTableColumn, Trait, TraitTypeEnum } from 'src/app/types/types';
import { AddDbaMatTableRecID } from 'src/app/util/utilGenericDataTable';

const TYPESTRING = "TypeString";
const CREATIONTIMESTRING = "CreationTimeString";

@Component({
    selector: 'app-trait-view',
    templateUrl: './trait-view.component.html',
    styleUrls: ['./trait-view.component.scss']
})
/** traitView component*/
export class TraitViewComponent {
    @Input() vorteilView: boolean = false;
    @Input() nachteilView: boolean = false;

    private currentFilter: TraitTypeEnum;

    public showButton: boolean = false;
    public tableName: string;
    public traitList: Trait[] = [];
    public dataSourceMain = new MatTableDataSource();
    public columnsdefMain: GenericDataTableColumn[];
    public attributData: Attribute[];
    public matOptionList: DBADataTableSelect[] = [];


    /** traitView ctor */
    constructor(private charakterService: CharakterService, private traitService: TraitService, private dialog: MatDialog) {
    }
    ngOnInit(): void {
        this.createTable();
        this.LoadDataMain();

        for (const value in TraitTypeEnum) {
            if (typeof TraitTypeEnum[value] !== "string") {
                var item = new DBADataTableSelect();
                item.label = value;
                item.id = TraitTypeEnum[value];
                this.matOptionList.push(item);
            }
        }
        this.currentFilter = TraitTypeEnum.Keiner;
    }

    private createTable() {
        var showShort = (this.nachteilView || this.vorteilView);
        this.showButton = !showShort;

        this.columnsdefMain = [
            { id: 'Name', label: 'Name', hideOrder: 0, width: 200 },
            { id: TYPESTRING, label: 'Typ', hideOrder: 0, width: 100, hideColumn: showShort },
            { id: 'GP', label: 'GP', hideOrder: 0, width: 40 },
            { id: 'Value', label: 'Value', hideOrder: 0, width: 50 },
            { id: 'LongDescription', label: 'Beschreibung', hideOrder: 0 },
            { id: CREATIONTIMESTRING, label: 'Erstellung', hideOrder: 0, width: 100, hideColumn: showShort }
        ]
    }

    public LoadDataMain() {
        this.traitService.GetList(this.charakterService.CurrentCharakter).then(result => {
            if (this.vorteilView) {
                this.tableName = "Vorteil";
                result = result.filter(x => x.Type == TraitTypeEnum.Vorteil);
                this.matOptionList = [];
            }
            if (this.nachteilView) {
                this.tableName = "Nachteil";
                result = result.filter(x => x.Type == TraitTypeEnum.Nachteil);
                this.matOptionList = [];
            }

            this.traitList = result;
            this.LoadDataMain2(result, this.currentFilter);
        })
    }

    private LoadDataMain2(dataList: Trait[], filter: TraitTypeEnum) {
        if (filter == TraitTypeEnum.Keiner) {
            var list = dataList;
        }
        else {
            var list = dataList.filter(x => x.Type == filter);
        }

        console.log(list);
        console.log(filter);

        this.dataSourceMain.data = AddDbaMatTableRecID<Trait>(list, (element) => {
            element[TYPESTRING] = TraitTypeEnum[element.Type];
            element[CREATIONTIMESTRING] = new Date(element.CreationDate).toLocaleDateString();
            return element;
        });

    }

    public openDialog(event: Trait[]) {
        this.PromiseGetTraitEvent(event[0]).then(result => {
            var config = new MatDialogConfig();
            config.data = result;

            let dialogRef = this.dialog.open(CreateTraitDialogComponent, config);
            var subscribtion = dialogRef.afterClosed().subscribe((trait: Trait) => {
                if (trait != null) {
                    this.traitService.Set(this.charakterService.CurrentCharakter, trait).then(result => {
                        this.traitList.push(trait);
                        this.LoadDataMain();
                    });
                }
                subscribtion.unsubscribe();
            });
        })
    }

    private PromiseGetTraitEvent(selectedElement: Trait): Promise<Trait> {
        if (selectedElement) return new Promise<Trait>(resolve => { resolve(selectedElement) })
        else {
            return this.traitService.GetNew(this.charakterService.CurrentCharakter);
        }
    }

    public SelectionChanged($event) {
        this.LoadDataMain2(this.traitList, $event);
    }
}