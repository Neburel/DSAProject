import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { Charakter } from 'src/app/types';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
/** loading component*/
export class LoadingComponent implements OnInit {
  /** loading ctor */
  constructor(private charakterService: CharakterService) { }
  @Output() Choosed: EventEmitter<Charakter> = new EventEmitter();
  public Loading: boolean = true;
  public CharakterList: Charakter[];

  ngOnInit(): void {
    this.Load();
  }

  private Load() {
    this.charakterService.GetList().then(result => {
      this.CharakterList = result;
      this.Loading = false;
    })
  }

  onSelect(charakter: Charakter): void {
    if (charakter == null) {
      this.charakterService.CreateCharakter().then(result => {
        this.emitEvent(result);
      });
    }
    else {
      this.emitEvent(charakter);
    };
  }

  private emitEvent(charakter: Charakter) {
    this.Choosed.emit(charakter);
  }
}

