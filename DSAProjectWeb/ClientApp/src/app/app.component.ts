import { Component, OnInit } from '@angular/core';

import { AppService } from './services/dsa/app.service';
import { Charakter } from './types';
import { CharakterService } from './services/dsa/charakter.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public loading = true;
  public charakterChoise = false;

  title = 'app';

  constructor(public appService: AppService, private charakterService: CharakterService) { }

  ngOnInit(): void {
    this.appService.Init().subscribe(subscriber => {
      this.loading = false;
    });
  }

  public charakterChosed(charakter: Charakter) {
    this.charakterService.SetCurrentCharakter(charakter).then(() => {
      this.loading = false;
      this.charakterChoise = true;
    });
  }
}
