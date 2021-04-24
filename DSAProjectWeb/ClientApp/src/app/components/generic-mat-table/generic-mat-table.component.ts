import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterContentInit, AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, NgZone, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { EventEmitter } from '@angular/core';
import { DBADataTableButton, GenericDataTableColumn, DBADataTableSelect, TextView } from 'src/app/types/types';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';
import { ViewportRuler } from '@angular/cdk/overlay';
import { DialogService } from 'src/app/services/base/dialog.service';
import { CreateDBADataTableButton } from 'src/app/util/utilGenericDataTable';
import { ExcelService } from 'src/app/services/base/excel.service';

export type DbaMatTableCallbackLoadData = () => void;
export type DbaMatTableCallbackDeletePromise<T> = (element: T) => Promise<void>;
export const buttonErstellenID: string = "Erstellen";
export const buttonBearbeitenID: string = "Bearbeiten";
export const buttonLöschenID: string = "Löschen";

export const DATATYPETEXTFREETEXT: string = "textfreeText";

@Component({
    selector: 'app-generic-mat-table',
    templateUrl: './generic-mat-table.component.html',
    styleUrls: ['./generic-mat-table.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [
        trigger('detailExpand', [
            state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
            state('expanded', style({ height: '*', visibility: 'visible' })),
            transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ],
})
/** GenericMatTable component*/
export class GenericMatTableComponent implements OnInit, AfterContentInit, AfterViewInit {
    @Input() tableName: string;
    @Input() editButton: boolean = false;
    @Input() createButton: boolean = false;
    @Input() deleteButton: boolean = false;
    @Input() exportButton: boolean = true;
    @Input() importButton: boolean = false;
    @Input() showWuerfelEingabe: boolean = false;
    @Input() showHeader: boolean = true;
    @Input() showSearch: boolean = true;
    @Input() showPaginator: boolean = true;
    @Input() component: any;
    @Input() dataSource: MatTableDataSource<any>;
    @Input() columnsdef: GenericDataTableColumn[];
    @Input() buttonsdef: DBADataTableButton[];
    @Input() pageSize: number = 25;
    @Input() matOptionTitle: string;
    @Input() matOptionList: DBADataTableSelect[] = [];
    @Input() selectedMatOption: string;
    @Input() hideButtons = false;
    @Input() isLoading = false;
    @Input() dbaMatTableCallbackOpenDialog: (matDialog: MatDialogRef<any>) => MatDialogRef<any>;
    @Input() fullscreen: boolean;

    @Output() rowClicked: EventEmitter<any> = new EventEmitter();
    @Output() dblClickRow: EventEmitter<any> = new EventEmitter();
    @Output() headerSelectChanged: EventEmitter<any> = new EventEmitter();
    @Output() importClicked: EventEmitter<any> = new EventEmitter();
    @Output() exportClicked: EventEmitter<any> = new EventEmitter();
    @Output() deleteClicked: EventEmitter<any> = new EventEmitter();
    @Output() probeInput: EventEmitter<any> = new EventEmitter();
    @Output() reload: EventEmitter<any> = new EventEmitter();
    @Output() searchFilterKeyboardInput: EventEmitter<KeyboardEvent> = new EventEmitter();
    @Output() openDialog: EventEmitter<any[]> = new EventEmitter();

    // MatTable
    @ViewChild('dataTable', { static: true }) dataTable: MatTable<Element>;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;

    // Allgemein
    public MIN_COLUMN_WIDTH: number = 200;
    private rulerSubscription: Subscription;
    public defaultButtonList: DBADataTableButton[] = [];


    isExpansionDetailRow = (index, item) => item.hasOwnProperty('detailRow');

    // Select
    public currentSelectedList: GenericDataTableColumn[] = [];

    // Filter Fields
    generalFilter = new FormControl

    // Visible Hidden Columns
    visibleColumns: GenericDataTableColumn[];
    hiddenColumns: GenericDataTableColumn[];
    expandedElement: any = {};

    // MatPaginator
    pageNumbers: number[];
    pageIndex: 1;
    length = 100;
    pageSizeOptions: number[] = [5, 10, 25, 100];
    pageEvent: PageEvent;
    goTo: number;

    // Getter
    get visibleColumnsIds() {
        const visibleColumnsIds = this.visibleColumns.map(column => column.id)
        return this.hiddenColumns.length ? ['trigger', ...visibleColumnsIds] : visibleColumnsIds
    }

    get hiddenColumnsIds() {
        return this.hiddenColumns.map(column => column.id)
    }


    /** GenericMatTable ctor */
    constructor(
        private excelService: ExcelService,
        private ruler: ViewportRuler,
        private _changeDetectorRef: ChangeDetectorRef,
        private zone: NgZone,
        private dialogService: DialogService,
        private renderer: Renderer2,
        private dialog: MatDialog) {
        this.rulerSubscription = this.ruler.change(100).subscribe(data => {
            // accesing clientWidth cause browser layout, cache it!
            // const tableWidth = this.table.nativeElement.clientWidth;
            this.toggleColumns(this.dataTable['_elementRef'].nativeElement.clientWidth);
        });
    }

    //#region  Lifecycle
    ngOnInit() {
        if (this.createButton) this.defaultButtonList.push(CreateDBADataTableButton(buttonErstellenID, buttonErstellenID, false, false, () => this.OpenDialog([])));
        if (this.editButton) this.defaultButtonList.push(CreateDBADataTableButton(buttonBearbeitenID, buttonBearbeitenID, false, true, (selected) => this.OpenDialog(selected)));
        if (this.deleteButton) this.defaultButtonList.push(CreateDBADataTableButton(buttonLöschenID, buttonLöschenID, false, true, (selected) => { this.deleteAction(selected); }));
        if (this.exportButton) this.defaultButtonList.push(CreateDBADataTableButton("Export", "Export", false, false, () => {
            this.export();
        }));
        if (this.importButton) this.defaultButtonList.push(CreateDBADataTableButton("Import", "Import", false, false, () => { this.importClicked.emit() }));

        if (this.fullscreen) {
            this.renderer.setStyle(this.dataTable['_elementRef'].nativeElement, 'height', 'calc(100vh - 250px)');
        }

        console.log(this.showSearch);
    }

    ngAfterContentInit() {
        this.toggleColumns(this.dataTable['_elementRef'].nativeElement.clientWidth);
        this.length = (this.dataSource.data.length);
        // this.dataSource.paginator = this.paginator.paginator;
        if (this.showPaginator) {
            this.dataSource.paginator = this.paginator;
        }
        this.dataSource.sort = this.sort;
        this.dataSource.sortingDataAccessor = (data: any, sortHeaderId: string) => {
            const value: any = data[sortHeaderId];
            return typeof value === "string" ? value.toLowerCase() : value;
        };
        // this.paginator.updateGoto();
        this.highlight(null, false); //führt dazu das die Buttons auf den Richtigen zustand gesetz werden
        this._changeDetectorRef.checkNoChanges();
    }

    ngAfterViewInit(): void {
        if (this.showPaginator) {
            this.dataSource.paginator = this.paginator;
        }
    }

    ngOnDestroy() {
        this.rulerSubscription.unsubscribe();
    }
    //#endregion Lifecycle

    applyFilter(event: KeyboardEvent) {
        this.isLoading = true;
        var filterValue = (event.target as any).value;
        filterValue = filterValue.trim(); // Remove whitespace
        filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
        this.dataSource.filter = filterValue;
        this.length = this.dataSource.sortData.length;

        this.searchFilterKeyboardInput.emit(event);
        this.isLoading = false;
    }
    probeValue(event: KeyboardEvent) {
        var value = (event.target as any).value;
        this.probeInput.emit(value);
    }

    clickExpandButton(row: any) {
        this.expandedElement[row.recId] = !this.expandedElement[row.recId];
        this._changeDetectorRef.detectChanges();
    }

    highlight(selectedElement: GenericDataTableColumn, emitEvent: boolean = true) {
        //das boolean für EmitEvent wird benötigt, damit nicht bei der Initialisierung das Event geworfen wird
        var contains = false;
        var allButtons: DBADataTableButton[] = [];
        allButtons = allButtons.concat(this.defaultButtonList);
        if (this.buttonsdef) {
            allButtons = allButtons.concat(this.buttonsdef);
        }
        var singleSelectButtonList = allButtons.filter(x => x.singleSelected == true);

        if (this.currentSelectedList.length > 0) {
            this.currentSelectedList.forEach(element => {
                if (element == selectedElement && element != null) {
                    contains = true;
                }
                if (element != null) {
                    element.highlighted = false;
                }
            });
            this.currentSelectedList = [];
        }
        if (!contains && selectedElement) {
            selectedElement.highlighted = !selectedElement.highlighted;
            this.currentSelectedList.push(selectedElement);
        }
        singleSelectButtonList.forEach(element => {
            element.disabled = !(this.currentSelectedList.length > 0);
        });
        if (emitEvent) this.rowClicked.emit(selectedElement);
        this._changeDetectorRef.detectChanges();
    }

    public resetSelection() {
        this.currentSelectedList.forEach(element => {
            element.highlighted = false;
        });
        this.currentSelectedList = [];
        this.highlight(null, false);
    }

    mouseover(row: GenericDataTableColumn) {
        row.hovered = true;
        this._changeDetectorRef.detectChanges();
    }

    mouseout(row: GenericDataTableColumn) {
        row.hovered = false;
        this._changeDetectorRef.detectChanges();
    }

    toggleColumns(tableWidth: number) {
        this.zone.runOutsideAngular(() => {
            const sortedColumns = this.columnsdef.slice()
                .map((column, index) => ({ ...column, order: index }))
                .sort((a, b) => a.hideOrder - b.hideOrder);

            for (const column of sortedColumns) {
                const columnWidth = column.width ? column.width : this.MIN_COLUMN_WIDTH;

                if (column.hideOrder && tableWidth < columnWidth) {
                    column.visible = false;
                    continue;
                }
                if (column.hideColumn) {
                    column.visible = false;
                    continue;
                }
                tableWidth -= columnWidth;
                column.visible = true;
            }

            this.columnsdef = sortedColumns.sort((a, b) => a.order - b.order);
            this.visibleColumns = this.columnsdef.filter(column => column.visible && !column.hideColumn);
            this.hiddenColumns = this.columnsdef.filter(column => !column.visible && !column.hideColumn);
        })

        this._changeDetectorRef.detectChanges();
    }

    public dblClick(row: any) {
        this.OpenDialog([row]);
        this.dblClickRow.emit(row);
    }

    public showColumnClick($event: MouseEvent, column: GenericDataTableColumn) {
        $event.stopPropagation()
        column.hideColumn = !column.hideColumn;
        this.toggleColumns(this.dataTable['_elementRef'].nativeElement.clientWidth);
    }

    public OpenDialog(data: any[]) {
        this.openDialog.emit(data);

        if (this.component) {
            const dialogRef = this.dialog.open(this.component, { data: data });
            var subscription = dialogRef.afterClosed().subscribe(() => {

                this.reload.emit();
                subscription.unsubscribe();
            });
        }
    }

    public deleteAction(selected) {
        this.dialogService.showOkCancelDialog("Test", "Test").subscribe(result => {
            if (result) {
                this.deleteClicked.emit(selected)
            }
        })
    }

    private export() {
        const clonedData = [];
        const data = this.dataSource.filteredData;

        data.forEach(val => {
            var sortedKeys = Object.keys(val).sort((x, y) => {
                //Sortieren der Properties für die Richtige Export Reihenfolge
                var columnX = this.columnsdef.indexOf(this.columnsdef.find(column => column.id == x));
                var columny = this.columnsdef.indexOf(this.columnsdef.find(column => column.id == y));

                if (columnX < columny) return -1;
                else if (columny > columnX) return 1;
                else return 0;
            });
            var newObject = new Object();
            sortedKeys.forEach((key) => {
                var def = this.columnsdef.find(x => x.id == key);

                if (def && !def.noexport) {

                    var label = def.label;
                    //Export
                    if (def.exportFunction) {
                        newObject[label] = def.exportFunction(val[key]);
                    }
                    else {
                        newObject[label] = val[key];
                    }
                    if (def.datatype == DATATYPETEXTFREETEXT) {
                        var textView = newObject[label] as any as TextView;
                        var text = textView.Text;
                        text = text + " " + textView.FreeText;
                        text.trim();
                        newObject[label] = text;
                    }

                }
            });
            clonedData.push(newObject);
        });
        this.excelService.exportAsExcelFile(clonedData, this.tableName);
    }
}
