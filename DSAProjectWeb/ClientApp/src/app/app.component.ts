import { Component, OnInit } from '@angular/core';

import { AppService } from './services/dsa/app.service';
import { Charakter } from './types/types';
import { CharakterService } from './services/dsa/charakter.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public loading = true;
  public charakter: Charakter;

  title = 'app';

  constructor(public appService: AppService, private charakterService: CharakterService) { }

  ngOnInit(): void {
    this.appService.Init().subscribe(subscriber => {
      this.loading = false;
    });
  }

  public charakterChosed(charakter: Charakter) {
    this.charakterService.SetCharakter(charakter).then(() => {
      this.loading = false;
      this.charakter = charakter;
    });
  }

  public abmelden(){
    this.charakter = null;
  }
}
