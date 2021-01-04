import { Component, Input, OnInit } from '@angular/core';
import { IDValueView } from 'src/app/types';

@Component({
    selector: 'app-id-value-table',
    templateUrl: './id-value-table.component.html',
    styleUrls: ['./id-value-table.component.scss']
})
/** IdValueTable component*/
export class IdValueTableComponent implements OnInit {
    @Input() data: IDValueView<any>;

    public displayedColumns: string[] = ['name', 'value', 'button'];
    public loading = true;
    /** IdValueTable ctor */
    constructor() {

    }
    ngOnInit() {
        this.loading = false;
    }
    public ClickButton(element: IDValueView<any>, value: number): void {
        element.Value = element.Value + value;
    }

}