import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Route } from '@angular/router';
import { GenericMatTableComponent } from 'src/app/components/generic-mat-table/generic-mat-table.component';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { TalentService } from 'src/app/services/dsa/talent.service';
import { Deduction, GenericDataTableColumn as GenericDataTableColumn, Talent, TalentTypeEnum } from 'src/app/types/types';
import { AddDbaMatTableRecID } from 'src/app/util/utilGenericDataTable';

const CURRENTPROBE = "CurrentProbe";
const WIDTHTAW = 100;
const WIDTHBE = 30;
@Component({
  selector: 'app-talent-view',
  templateUrl: './talent-view.component.html',
  styleUrls: ['./talent-view.component.scss']
})
/** TalentView component*/
export class TalentViewComponent implements OnInit {
  @ViewChild(GenericMatTableComponent, { static: true }) dataTableMain: GenericMatTableComponent;
  public dataSourceMain = new MatTableDataSource();
  public columnsdefMain: GenericDataTableColumn[];
  private routeParam: TalentTypeEnum;

  /** TalentView ctor */
  constructor(private charakterService: CharakterService, public talentService: TalentService, private actRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.columnsdefMain = null;
    this.actRoute.paramMap.subscribe(params => {
      this.routeParam = Number(params.get('id')) as TalentTypeEnum;
      this.createTable();
      this.LoadDataMain();
    });
  }

  private createTable() {
    var showAT = !(this.routeParam == TalentTypeEnum.close || this.routeParam == TalentTypeEnum.weaponless || this.routeParam == TalentTypeEnum.range);
    var showPA = !(this.routeParam == TalentTypeEnum.close || this.routeParam == TalentTypeEnum.weaponless);
    var showBL = !(this.routeParam == TalentTypeEnum.close || this.routeParam == TalentTypeEnum.weaponless);
    var probeString = !(showAT || showPA || showBL);
    var ableitungText = "Ableiten (+10)";
    if (probeString) {
      ableitungText = "Verwandte Fertigkeiten (+5)"
    }
    this.columnsdefMain = [
      { id: 'Name', label: 'Name', hideOrder: 0 },
      { id: 'Probe', label: 'Probe', hideOrder: 0, width: 80, noexport: true },
      { id: 'ProbeString', label: 'Probe', hideOrder: 0, width: 100, hideColumn: probeString },
      {
        id: 'TAW', label: 'TAW', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'Name', clickEvent: (talent: Talent, input: number) => {
          talent.TAW = talent.TAW + input;
          this.SetTaw(talent);
        }
      },
      {
        id: 'AT', label: 'AT', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'Name', hideColumn: showAT, noexport: showAT, clickEvent: (talent: Talent, input: number) => {
          talent.AT = talent.AT + input;
          this.SetTaw(talent);
        }
      },
      {
        id: 'PA', label: 'PA', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'Name', hideColumn: showPA, noexport: showPA, clickEvent: (talent: Talent, input: number) => {
          talent.PA = talent.PA + input;
          this.SetTaw(talent);
        }
      },
      {
        id: 'BL', label: 'BL', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'Name', hideColumn: showBL, noexport: showBL, clickEvent: (talent: Talent, input: number) => {
          talent.BL = talent.BL + input;
          this.SetTaw(talent);
        }
      },
      { id: 'BE', label: 'BE', hideOrder: 0, width: WIDTHBE, datatype: "small", hideColumn: true },
      { id: 'Spezialisierung', label: 'Spezialisierung', hideOrder: 0, datatype: "small" },
      { id: 'Waffenmeister', label: 'Waffenmeister', hideOrder: 0, datatype: "small", hideColumn: !probeString, noexport: !probeString },
      { id: "RequirementText", label: 'Anforderung', hideOrder: 0, datatype: "textfreeText", hideColumn: probeString, noexport: probeString },
      { id: "DeductionText", label: ableitungText, hideOrder: 0, datatype: "textfreeText" },
      {
        id: "A", label: 'Ableitung(Auswahl)', hideOrder: 0, datatype: "selection", selectedValueID: "DeductionSelected", selectionListID: "DeductionList", clickEvent: (talent: Talent, selected: string) => {
          var deduction = new Deduction();
          if(selected && selected != ""){
            deduction.ID = selected;
          }
          else{
            deduction = null;
          }

          talent.DeductionSelected = deduction
          this.SetTalent(talent);
        }
      },
      {
        id: CURRENTPROBE, label: '', hideOrder: 0, width: 80, datatype: "small"
      },
    ]
  }

  public LoadDataMain() {
    this.dataTableMain.isLoading = true;
    this.talentService.GetTalentViewList(this.charakterService.CurrentCharakter, this.routeParam).then(dataList => {
      this.dataSourceMain.data = AddDbaMatTableRecID<Talent>(dataList, (element) => {
        element[CURRENTPROBE] = 0;
        return element;
      });
      this.dataTableMain.isLoading = false;
    })
  }

  public SetTaw(talent: Talent) {
    this.talentService.SetTalent(this.charakterService.CurrentCharakter, talent).then(result => {
      var updateItem = this.dataSourceMain.data.find((x: Talent) => x.ID == result.ID) as Talent;
      updateItem.Probe = result.Probe;
      this.dataSourceMain._updateChangeSubscription();
    })
  }

  public SetTalent(talent: Talent) {
    this.talentService.SetTalent(this.charakterService.CurrentCharakter, talent).then(result => {
      this.LoadDataMain();
    })
  }

  public probeInput(value: number) {
    this.dataTableMain.dataSource.data.forEach((element: Talent) => {
      if (Number(element.Probe)) {
        element[CURRENTPROBE] = (Number(element.Probe)) - value;
      }
      else {
        var split = element.Probe.split("/");
        var string = "";
        split.forEach(element => {
          element = element.trim();
          var x = (Number(element)) - value;
          if (string != "") {
            string = string + '\\';
          }
          string = string + x;
        });
        element[CURRENTPROBE] = string;
      }
    });
  }
}