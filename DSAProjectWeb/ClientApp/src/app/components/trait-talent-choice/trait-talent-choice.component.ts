import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSelect } from '@angular/material/select';

import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { Talent } from 'src/app/types';

@Component({
  selector: 'app-trait-talent-choice',
  templateUrl: './trait-talent-choice.component.html',
  styleUrls: ['./trait-talent-choice.component.scss']
})
export class TraitTalentChoiceComponent {
  @Input() talentList: Talent[];
  @Output() talentSelected: EventEmitter<Talent> = new EventEmitter<Talent>();

  public talentControl: FormControl = new FormControl();
  public talentFilterControl: FormControl = new FormControl();

  /** list of banks filtered by search keyword */
  public filteredTalentList: ReplaySubject<Talent[]> = new ReplaySubject<Talent[]>(1);

  @ViewChild('singleSelect') singleSelect: MatSelect;

  /** Subject that emits when the component has been destroyed. */
  private _onDestroy = new Subject<void>();

  ngOnInit() {
    // set initial selection
    this.talentControl.setValue(null);

    // load the initial bank list
    this.filteredTalentList.next(this.talentList.slice());

    // listen for search field value changes
    this.talentFilterControl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filterTalentList();
      });
  }

  ngAfterViewInit() {
    this.setInitialValue();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

  private setInitialValue() {
    this.filteredTalentList
      .pipe(take(1), takeUntil(this._onDestroy))
      .subscribe(() => {
        this.singleSelect.compareWith = (a: Talent, b: Talent) => {
          if (!a) return true;
          if (!b) return false;

          return a.Name === b.Name;
        }
      });
  }

  private filterTalentList() {
    if (!this.talentList) {
      return;
    }
    // get the search keyword
    let search = this.talentFilterControl.value;
    if (!search) {
      this.filteredTalentList.next(this.talentList.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the banks
    this.filteredTalentList.next(
      this.talentList.filter(talent => talent.Name.toLowerCase().indexOf(search) > -1)
    );
  }

  public TalentSelected() {
    var currentTalent = this.talentControl.value;
    if (!currentTalent) return;
    this.talentSelected.emit(currentTalent);
  }

}
