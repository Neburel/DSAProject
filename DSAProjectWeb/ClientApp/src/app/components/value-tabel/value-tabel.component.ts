import { Component, OnInit } from '@angular/core';
import { AttributService } from 'src/app/services/dsa/attribut.service';
import { ValueService } from 'src/app/services/dsa/value.service';
import { DSADataSource, Value } from 'src/app/types';

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

    constructor(private attributService: AttributService, private valueService: ValueService) { }

    ngOnInit(): void {
        this.attributService.subject.subscribe(resolve => {
            this.Load();
        });

        this.Load();
    }

    private Load() {
        this.valueService.GetList(1).then(result => {
            console.log(result);
            var dataSource = new DSADataSource<Value>();
            dataSource.setData(result);
            this.dataSource = dataSource;
            this.loading = false;
        });
    }
}