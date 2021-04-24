import { Component, OnInit } from '@angular/core';
import { AttributService } from 'src/app/services/dsa/attribut.service';
import { Attribut, DSADataSource } from 'src/app/types/types';
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
  public total: Attribut;
  /** AttributTabel ctor */
  constructor(private charakterService: CharakterService, private attributService: AttributService) { }

  ngOnInit(): void {
    this.Load();
  }

  private Load() {
    this.total = new Attribut();
    this.total.Name = "Gesamt";
    this.total.AKT = 0;
    this.total.MOD = 0;
    this.total.MAX = 0;
    this.total["nobutton"] = true; //Quick Fix... sollte überarbeitet werden

    this.attributService.GetAttributList(this.charakterService.CurrentCharakter).then(result => {
      var dataSource = new DSADataSource<Attribut>();

      result.forEach(element => {
        this.total.AKT = this.total.AKT + element.AKT;
        this.total.MOD = this.total.MOD + element.MOD;
        this.total.MAX = this.total.MAX + element.MAX;
      });
      result.push(this.total);
      dataSource.setData(result);
      this.dataSource = dataSource;
      this.loading = false;
    });
  }

  public ClickButton(element: Attribut, value: number): void {
    var newValue = element.AKT + value;
    this.attributService.SetAttributAkt(this.charakterService.CurrentCharakter, element.ID, newValue).then(result => {
      this.Load();
    });
  }
}
