import { Component, OnInit } from '@angular/core';
import { AttributService } from 'src/app/services/dsa/attribut.service';
import { Attribut, DSADataSource } from 'src/app/types';
import { CharakterService } from 'src/app/services/dsa/charakter.service';

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
  constructor(private charakterService: CharakterService, private attributService: AttributService) { }

  ngOnInit(): void {
    this.Load();
  }

  private Load() {
    this.attributService.GetAttributList(this.charakterService.CurrentCharakter).then(result => {
      var dataSource = new DSADataSource<Attribut>();
      dataSource.setData(result);
      this.dataSource = dataSource;
      this.loading = false;
    });
  }

  public ClickButton(element: Attribut, value: number): void {
    var newValue = element.AKT + value;
    
    console.log(element);
    console.log(value);
    console.log(newValue)

    this.attributService.SetAttributAkt(this.charakterService.CurrentCharakter, element.ID, newValue).then(result => {
      console.log(result);
      this.Load();
    });
  }
}