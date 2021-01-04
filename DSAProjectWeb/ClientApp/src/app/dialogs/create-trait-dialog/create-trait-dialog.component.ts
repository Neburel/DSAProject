import { Component, Inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { GenericMatTableComponent } from 'src/app/components/generic-mat-table/generic-mat-table.component';
import { TraitTalentChoiceComponent } from 'src/app/components/trait-talent-choice/trait-talent-choice.component';
import { DialogService } from 'src/app/services/base/dialog.service';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { TalentService } from 'src/app/services/dsa/talent.service';
import { GenericDataTableColumn, IDValueView, Talent, TalentTypeEnum, Trait, TraitTypeEnum } from 'src/app/types';
import { AddDbaMatTableRecID } from 'src/app/util/utilGenericDataTable';

const WIDTHTAW = 100;

@Component({
    selector: 'app-create-trait-dialog',
    templateUrl: './create-trait-dialog.component.html',
    styleUrls: ['./create-trait-dialog.component.scss']
})
/** createTraitDialog component*/
export class CreateTraitDialogComponent {
    @ViewChild(GenericMatTableComponent, { static: true }) dataTableMain: GenericMatTableComponent;

    public Title = "Erstelle Eigenschaft";
    public Loading: Boolean = true;
    public Wert: string = "";
    public GP: string = "";
    public TalentListComplete: Talent[];
    public TalentListAT: Talent[];
    public TalentListPA: Talent[];
    public TalentListBL: Talent[];
    public TalentListTrait: Talent[] = [];
    public TraitTypeEnum = TraitTypeEnum;
    public TraitTypeList = Object.values(TraitTypeEnum).filter(value => typeof value === 'number');

    public dataSourceTraitTalent = new MatTableDataSource();
    public columnsdefMain: GenericDataTableColumn[];

    public Attribute: IDValueView<number>[];
    public Values: IDValueView<string>[];
    public Resource: IDValueView<string>[];

    private typeControl: FormControl = new FormControl('', [Validators.required]);
    private nameControl: FormControl = new FormControl('', [Validators.required]);
    private descriptionControl: FormControl = new FormControl('');
    private apGainControl: FormControl = new FormControl('', [Validators.required]);
    private apInvestControl: FormControl = new FormControl('', [Validators.required]);

    public FormGroup = new FormGroup({
        type: this.typeControl,
        name: this.nameControl,
        description: this.descriptionControl,
        apGain: this.apGainControl,
        apInvest: this.apInvestControl,
    });

    /** createTraitDialog ctor */
    constructor(
        private charakterService: CharakterService,
        private talentService: TalentService,
        private dialogService: DialogService,
        public dialogRef: MatDialogRef<CreateTraitDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Trait) {
    }

    ngOnInit() {
        var generalTalentPromise = this.talentService.GetTalentList(this.charakterService.CurrentCharakter, TalentTypeEnum.general);
        var languageTalentPromise = this.talentService.GetTalentList(this.charakterService.CurrentCharakter, TalentTypeEnum.language);
        var closePromise = this.talentService.GetTalentList(this.charakterService.CurrentCharakter, TalentTypeEnum.close);
        var rangePromise = this.talentService.GetTalentList(this.charakterService.CurrentCharakter, TalentTypeEnum.range);
        var weaponlessPromise = this.talentService.GetTalentList(this.charakterService.CurrentCharakter, TalentTypeEnum.weaponless);

        this.createTable();

        Promise.all([generalTalentPromise, languageTalentPromise, closePromise, rangePromise, weaponlessPromise]).then(result => {
            var generalTalentList = result[0];
            var languageTalentList = result[1];
            var closeTalentList = result[2];
            var rangeTalentList = result[3];
            var weaponlessTalentList = result[4];

            languageTalentList.forEach(element => {
                element.Name = element.Name + "(" + element.NameExtension + ")";
                //Schnellfix statt vernünftige Lösung, da wenig Nutzen, kann verbessert werden
            });

            var list = generalTalentList;
            list = list.concat(languageTalentList);
            list = list.concat(closeTalentList);
            list = list.concat(rangeTalentList);
            list = list.concat(weaponlessTalentList);

            this.TalentListComplete = list.sort((a, b) => a.Name.localeCompare(b.Name));
            this.TalentListAT = closeTalentList.concat(rangeTalentList).concat(weaponlessTalentList);
            this.TalentListPA = closeTalentList.concat(weaponlessTalentList);
            this.TalentListBL = closeTalentList.concat(weaponlessTalentList);

            if (this.data) {
                this.nameControl.setValue(this.data.Name);
                this.descriptionControl.setValue(this.data.Description);
                this.apGainControl.setValue(this.data.APGain);
                this.apInvestControl.setValue(this.data.APInvest);
                this.typeControl.setValue(this.data.Type);
                this.GP = this.data.GP;
                this.Wert = this.data.Value;
                this.Attribute = this.data.AttributList;
                this.Values = this.data.ValueList;
                this.Resource = this.data.ResourceList;
                this.TalentListTrait = this.data.TalentList;
            }

            this.LoadDataMain();
            this.Loading = false;
        }).catch(error => {
            this.dialogService.showMessageDialog("Fehler", error)
        });
    }
    ngAfterContentInit(): void {
        this.dataSourceTraitTalent.data = [];
    }

    private createTable() {
        this.columnsdefMain = [
            { id: 'Name', label: 'Name', hideOrder: 0, width: 150 },
            {
                id: 'TAW', label: 'TAW', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'Name', clickEvent: (talent: Talent, input: number) => {
                    talent.TAW = talent.TAW + input;
                }
            },
            {
                id: 'AT', label: 'AT', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'AT', clickEvent: (talent: Talent, input: number) => {
                    talent.AT = talent.AT + input;
                }
            },
            {
                id: 'PA', label: 'PA', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'PA', clickEvent: (talent: Talent, input: number) => {
                    talent.PA = talent.PA + input;
                }
            },
            {
                id: 'BL', label: 'BL', hideOrder: 0, datatype: 'taw', width: WIDTHTAW, nullCheckID: 'BL', clickEvent: (talent: Talent, input: number) => {
                    talent.BL = talent.BL + input;
                }
            },
            {
                id: 'Delete', label: '', hideOrder: 0, datatype: 'xButton', width: 60, clickEvent: (talent: Talent) => {
                    const index: number = this.TalentListTrait.indexOf(talent);
                    if (index !== -1) {
                        if (this.TalentListTrait.length == 1) this.TalentListTrait = [];
                        else {
                            this.TalentListTrait = this.TalentListTrait.splice(index, 1);
                        }

                        this.LoadDataMain();
                    }
                }
            }
        ]
    }

    public LoadDataMain() {
        this.dataSourceTraitTalent.data = AddDbaMatTableRecID<Talent>(this.TalentListTrait, (element) => {
            element.AT = this.TalentListAT.find(x => x.ID == element.ID) ? 0 : null;;
            element.PA = this.TalentListPA.find(x => x.ID == element.ID) ? 0 : null;;
            element.BL = this.TalentListBL.find(x => x.ID == element.ID) ? 0 : null;;
            return element;
        });
    }

    public TalentSelected(talent: Talent) {
        if (this.TalentListTrait.find(x => x.ID == talent.ID)) return;
        talent.TAW = 0;

        this.TalentListTrait = this.TalentListTrait.concat(talent);
        this.LoadDataMain();
    }

    public SetWert(value: any) {
        this.Wert = this.SetHelper(this.Wert, value);
    }

    public SetGP(value: any) {
        this.GP = this.SetHelper(this.GP, value);
    }

    public SetHelper(variable: any, value: any) {
        var numberValue = Number(value);
        if (numberValue) {
            var currentValue = Number(variable);
            if (isNaN(currentValue)) variable = "0";
            else variable = currentValue + value;
        }
        else {
            variable = value;
        }
        return variable;
    }

    public Cancel() {
        this.dialogRef.close();
    }

    public Save() {
        this.FormGroup.markAllAsTouched();
        if (this.FormGroup.invalid) return;

        var trait = new Trait();
        trait.Name = this.nameControl.value;
        trait.Type = this.typeControl.value;
        trait.APGain = this.apGainControl.value;
        trait.APInvest = this.apInvestControl.value;
        trait.GP = this.GP.toString();
        trait.Value = this.Wert.toString();

        trait.AttributList = this.Attribute;
        trait.ValueList = this.Values;
        trait.ResourceList = this.Resource;
        trait.TalentList = this.TalentListTrait;

        if(this.data){
            trait.ID = this.data.ID;
        }

        this.dialogRef.close(trait);
    }
}