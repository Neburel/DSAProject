import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatMenuModule, MatToolbarModule, MatIconModule, MatCardModule, MatCheckboxModule, MatDialogModule, MatSidenavModule, MatProgressSpinnerModule, MatTableModule, MatFormFieldModule, MatSelectModule, MatListModule, MatExpansionModule, MatDatepickerModule, MatStepperModule, MatInputModule, MatNativeDateModule } from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AttributTabelComponent } from '../app/components/attribut-tabel/attribut-tabel.component';
import { MessageDialogComponent } from './dialogs/message-dialog/message-dialog.component';
import { ResourceTabelComponent } from './components/resource-tabel/resource-tabel.component';
import { ValueTabelComponent } from './components/value-tabel/value-tabel.component';
import { LoadingComponent } from './views/loading/loading.component';
import { AdventureTabelComponent } from './components/adventure-tabel/adventure-tabel.component';
import { ApInputDialogComponent } from './dialogs/ap-input-dialog/ap-input-dialog.component';
import { BankComponent } from './components/bank/bank.component';
import { SettingsViewComponent } from './views/settings-view/settings-view.component';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,

    MessageDialogComponent,
    ApInputDialogComponent,

    SettingsViewComponent,

    LoadingComponent,
    AttributTabelComponent,
    ResourceTabelComponent,
    ValueTabelComponent,
    AdventureTabelComponent,
    BankComponent,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CdkTableModule,
    ReactiveFormsModule,
    FlexLayoutModule,

    MatButtonModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatMenuModule,
    MatToolbarModule,
    MatListModule,
    MatExpansionModule,
    MatDialogModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatStepperModule,
    MatInputModule,
    MatNativeDateModule,
    MatSidenavModule,

    RouterModule.forRoot([
      { path: 'settings', component: SettingsViewComponent, pathMatch: 'full' },
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ]),
    NoopAnimationsModule
  ],
  exports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatMenuModule,
    MatToolbarModule,
    MatListModule,
    MatExpansionModule,
    MatDialogModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatStepperModule,
    MatInputModule,
    MatNativeDateModule,
    MatSidenavModule
  ],
  entryComponents: [
    MessageDialogComponent,
    ApInputDialogComponent,
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
