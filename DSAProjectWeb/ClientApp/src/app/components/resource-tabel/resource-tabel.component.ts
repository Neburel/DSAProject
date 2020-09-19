﻿import { Component, OnInit } from '@angular/core';
import { ResourceService } from 'src/app/services/dsa/resource.service';
import { Resource, DSADataSource } from 'src/app/types';
import { AttributService } from 'src/app/services/dsa/attribut.service';

@Component({
    selector: 'app-resource-tabel',
    templateUrl: './resource-tabel.component.html',
    styleUrls: ['./resource-tabel.component.scss']
})
/** ResourceTabel component*/
export class ResourceTabelComponent implements OnInit {
    public displayedColumns: string[] = ['name', 'min', 'mod', 'max'];
    public loading = true;
    public dataSource;

    constructor(private attributService: AttributService, private resourceService: ResourceService) { }

    ngOnInit(): void {
        this.attributService.subject.subscribe(resolve => {
            this.Load();
        });

        this.Load();
    }

    private Load() {
        this.resourceService.GetList(1).then(result => {
            console.log(result);
            var dataSource = new DSADataSource<Resource>();
            dataSource.setData(result);
            this.dataSource = dataSource;
            this.loading = false;
        });
    }
}