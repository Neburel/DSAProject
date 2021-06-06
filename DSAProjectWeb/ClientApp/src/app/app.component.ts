import { Component, HostBinding, OnInit, ViewEncapsulation } from '@angular/core';

import { AppService } from './services/dsa/app.service';
import { Charakter } from './types/types';
import { CharakterService } from './services/dsa/charakter.service';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { OverlayContainer } from '@angular/cdk/overlay';
import { ColorSchemeService } from './services/util/color-scheme.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public loading = true;
  public charakter: Charakter;


  constructor(public appService: AppService, private charakterService: CharakterService, private colorSchemeService: ColorSchemeService, private overlay: OverlayContainer) { }

  ngOnInit(): void {
    this.colorSchemeService.load();

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

  public abmelden() {
    this.charakter = null;
  }
}
