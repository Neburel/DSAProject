import { Attribute, Component } from '@angular/core';
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
    public traitList: Trait[] = [];
    public dataSourceMain = new MatTableDataSource();
    public columnsdefMain: GenericDataTableColumn[];
    public attributData: Attribute[];

    /** traitView ctor */
    constructor(private charakterService: CharakterService, private traitService: TraitService, private dialog: MatDialog) {
        this.createTable();
        this.LoadDataMain();
    }
    ngOnInit(): void {
        this.LoadDataMain();
    }

    private createTable() {
        this.columnsdefMain = [
            { id: 'Name', label: 'Name', hideOrder: 0, width: 300 },
            { id: TYPESTRING, label: 'Typ', hideOrder: 0, width: 100 },
            { id: 'GP', label: 'GP', hideOrder: 0, width: 60 },
            { id: 'Value', label: 'Value', hideOrder: 0, width: 60 },
            { id: 'LongDescription', label: 'Beschreibung', hideOrder: 0 },
            { id: CREATIONTIMESTRING, label: 'Erstellung', hideOrder: 0, width: 100 }
        ]
    }

    public LoadDataMain() {
        this.traitService.GetList(this.charakterService.CurrentCharakter).then(result => {
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
        console.log(selectedElement);

        if (selectedElement) return new Promise<Trait>(resolve => { resolve(selectedElement) })
        else {
            return this.traitService.GetNew(this.charakterService.CurrentCharakter);
        }
    }
}