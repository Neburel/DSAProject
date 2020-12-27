import { Component, OnInit } from '@angular/core';
import { AttributService } from 'src/app/services/dsa/attribut.service';
import { ValueService } from 'src/app/services/dsa/value.service';
import { DSADataSource, Value } from 'src/app/types';
import { CharakterService } from 'src/app/services/dsa/charakter.service';

@Component({
    selector: 'app-value-tabel',
    templateUrl: './value-tabel.component.html',
    styleUrls: ['./value-tabel.component.scss']
})
/** ValueTabel component*/
export class ValueTabelComponent implements OnInit {
    public displayedColumns: string[] = ['name', 'min', 'mod', 'max'];
    public loading = true;
    public dataSource;

    constructor(
        private charakterService: CharakterService,
        private attributService: AttributService,
        private valueService: ValueService) { }

    ngOnInit(): void {
        this.attributService.AttributChanged.subscribe(resolve => {
            this.Load();
        });

        this.Load();
    }

    private Load() {
        this.valueService.GetList(this.charakterService.CurrentCharakter).then(result => {
            var dataSource = new DSADataSource<Value>();
            dataSource.setData(result);
            this.dataSource = dataSource;
            this.loading = false;
        });
    }
}