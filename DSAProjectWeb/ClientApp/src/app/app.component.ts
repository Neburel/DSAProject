import { Component, OnInit } from '@angular/core';
import { AppService } from './services/dsa/app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  public loading = true;
  title = 'app';

  constructor(public appService: AppService) { }

  ngOnInit(): void {
    this.appService.Init().subscribe(subscriber => {
      this.loading = false;
    });
  }
}
