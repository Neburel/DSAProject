import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatMenuModule, MatToolbarModule, MatIconModule, MatCardModule, MatCheckboxModule, MatDialogModule } from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AttributTabelComponent } from '../app/components/attribut-tabel/attribut-tabel.component';
import { DialogService } from './services/base/dialog.service';
import { MessageDialogComponent } from './dialogs/message-dialog/message-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,

    MessageDialogComponent,

    AttributTabelComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CdkTableModule,
    MatButtonModule,
    MatCheckboxModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ]),
    NoopAnimationsModule
  ],
  exports: [
    MatButtonModule,
    MatCheckboxModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,

    MessageDialogComponent,
  ],
  entryComponents: [
    MessageDialogComponent,
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
