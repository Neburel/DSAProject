import { DataSource } from '@angular/cdk/collections';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AttributService } from 'src/app/services/dsa/attribut.service';
import { Attribut, DSADataSource } from 'src/app/types';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-attribut-tabel',
  templateUrl: './attribut-tabel.component.html',
  styleUrls: ['./attribut-tabel.component.scss']
})
/** AttributTabel component*/
export class AttributTabelComponent implements OnInit {
  public displayedColumns: string[] = ['name', 'min', 'mod', 'max', 'button'];
  public loading = true;
  public dataSource;

  /** AttributTabel ctor */
  constructor(private attributService: AttributService) { }

  ngOnInit(): void {
    this.LOad();
  }

  private LOad() {
    this.attributService.GetAttributList(1).then(result => {
      console.log(result);
      var dataSource = new DSADataSource<Attribut>();
      dataSource.setData(result);
      this.dataSource = dataSource;
      this.loading = false;
    });
  }

  public ClickButton(element: Attribut, value: number): void {
    console.log(element);
    this.attributService.SetAttributAkt(1, element.ID, element.AKT + value).then(result => {
      console.log(result);
      this.LOad();
    });
  }
}
