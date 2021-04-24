import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CdkTableModule } from '@angular/cdk/table'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './views/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
import { TalentViewComponent } from './views/talent-view/talent-view.component';
import { MaterialModule } from './util/material.module';
import { GenericMatTableComponent } from './components/generic-mat-table/generic-mat-table.component';
import { LanguageViewComponent } from './views/language-view/language-view.component';
import { CreateTraitDialogComponent } from './dialogs/create-trait-dialog/create-trait-dialog.component';
import { TraitViewComponent } from './views/trait-view/trait-view.component';
import { IdValueTableComponent } from './components/id-value-table/id-value-table.component';
import { TraitTalentChoiceComponent } from './components/trait-talent-choice/trait-talent-choice.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { DescriptionComponent } from './components/description/description.component';
import { DescriptionDialogComponent } from './dialogs/description-dialog/description-dialog.component';
import { BankDialogComponent } from './dialogs/bank-dialog/bank-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GenericMatTableComponent,
    IdValueTableComponent,
    TraitTalentChoiceComponent,

    MessageDialogComponent,
    ApInputDialogComponent,

    SettingsViewComponent,

    LoadingComponent,
    AttributTabelComponent,
    ResourceTabelComponent,
    ValueTabelComponent,
    AdventureTabelComponent,
    BankComponent,
    TalentViewComponent,
    LanguageViewComponent,
    TraitViewComponent,
    DescriptionComponent,

    TraitTalentChoiceComponent,
    CreateTraitDialogComponent,
    DescriptionDialogComponent,
    BankDialogComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    CdkTableModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MaterialModule,
    NgxMatSelectSearchModule,

    RouterModule.forRoot([
      { path: 'settings', component: SettingsViewComponent, pathMatch: 'full' },
      { path: 'talentletterGeneral/:id', component: TalentViewComponent, pathMatch: 'full' },
      { path: 'talentletterRange/:id', component: TalentViewComponent, pathMatch: 'full' },
      { path: 'talentletterFighting/:id', component: TalentViewComponent, pathMatch: 'full' },
      { path: 'language', component: LanguageViewComponent, pathMatch: 'full' },
      { path: 'traitpage', component: TraitViewComponent, pathMatch: 'full' },
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ], { onSameUrlNavigation: 'reload' }),
  ],
  exports: [
    ReactiveFormsModule,
    MaterialModule,
    GenericMatTableComponent,
  ],
  entryComponents: [
    MessageDialogComponent,
    ApInputDialogComponent,
    GenericMatTableComponent,
    AttributTabelComponent,
    IdValueTableComponent,
    DescriptionDialogComponent,
    BankDialogComponent,
    TraitViewComponent
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
