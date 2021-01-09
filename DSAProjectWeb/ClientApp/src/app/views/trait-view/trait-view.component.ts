import { Attribute, Component, Input } from '@angular/core';
import { ShowHideStyleBuilder } from '@angular/flex-layout';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CreateTraitDialogComponent } from 'src/app/dialogs/create-trait-dialog/create-trait-dialog.component';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { TraitService } from 'src/app/services/dsa/trait.service';
import { GenericDataTableColumn, Trait, TraitTypeEnum } from 'src/app/types';
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

    public showButton: boolean = false;
    public tableName: string;
    public traitList: Trait[] = [];
    public dataSourceMain = new MatTableDataSource();
    public columnsdefMain: GenericDataTableColumn[];
    public attributData: Attribute[];

    /** traitView ctor */
    constructor(private charakterService: CharakterService, private traitService: TraitService, private dialog: MatDialog) {
    }
    ngOnInit(): void {
        this.createTable();
        this.LoadDataMain();
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
            }
            if (this.nachteilView) {
                this.tableName = "Nachteil";
                result = result.filter(x => x.Type == TraitTypeEnum.Nachteil);
            }

            this.traitList = result;
            this.dataSourceMain.data = AddDbaMatTableRecID<Trait>(this.traitList, (element) => {
                element[TYPESTRING] = TraitTypeEnum[element.Type];
                element[CREATIONTIMESTRING] = new Date(element.CreationDate).toLocaleDateString();
                return element;
            });
        })
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
}