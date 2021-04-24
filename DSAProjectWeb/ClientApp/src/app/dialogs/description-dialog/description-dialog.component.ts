import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Description, FamilienstatusEnum, GeschlechtEnum } from 'src/app/types/types';

@Component({
    selector: 'app-description-dialog',
    templateUrl: './description-dialog.component.html',
    styleUrls: ['./description-dialog.component.scss']
})
/** DescriptionDialog component*/
export class DescriptionDialogComponent {
    public Title = "Charakter";
    public GeschlechtEnum = GeschlechtEnum;
    public GeschlechtList = Object.values(GeschlechtEnum).filter(value => typeof value === 'number');
    public FamilienstatusEnum = FamilienstatusEnum;
    public FamilienstatusList = Object.values(FamilienstatusEnum).filter(value => typeof value === 'number');

    private nameControl: FormControl = new FormControl('');
    private augenControl: FormControl = new FormControl('');
    private groesseControl: FormControl = new FormControl('');
    private famlienstatusControl: FormControl = new FormControl('' );
    private anredeControl: FormControl = new FormControl('');    
    private kulurControl: FormControl = new FormControl('');
    private glaubeControl: FormControl = new FormControl('');
    private alterControl: FormControl = new FormControl('');
    private hautfarbeControl: FormControl = new FormControl('');
    private haarfarbeControl: FormControl = new FormControl('');
    private professionControl: FormControl = new FormControl('');
    private gewichtControl: FormControl = new FormControl('');
    private geschlechtControl: FormControl = new FormControl('' );
    private rasseControl: FormControl = new FormControl('' );

    public FormGroup = new FormGroup({
        name: this.nameControl,
        augen: this.augenControl,
        groesse: this.groesseControl,
        familienstatus: this.famlienstatusControl,
        geschlecht: this.geschlechtControl,
        anrede: this.anredeControl,
        kultur: this.kulurControl,
        glaube: this.glaubeControl,
        alter: this.alterControl,
        hautfarbe: this.hautfarbeControl,
        haarfarbe: this.haarfarbeControl,
        profession: this.professionControl,
        gewicht: this.gewichtControl,
        rasse: this.rasseControl,
    });

    constructor(public dialogRef: MatDialogRef<DescriptionDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: Description) {
        this.nameControl.setValue(data.Name);
        this.augenControl.setValue(data.Augenfarbe);
        this.groesseControl.setValue(data.Groesse);
        this.famlienstatusControl.setValue(data.Familienstatus);
        this.anredeControl.setValue(data.Anrede);
        this.kulurControl.setValue(data.Kultur);
        this.glaubeControl.setValue(data.Glaube);
        this.alterControl.setValue(data.Alter);
        this.hautfarbeControl.setValue(data.Hautfarbe);
        this.haarfarbeControl.setValue(data.Haarfarbe);
        this.professionControl.setValue(data.Profession);
        this.rasseControl.setValue(data.Rasse);
        this.gewichtControl.setValue(data.Gewicht);
        this.geschlechtControl.setValue(data.Geschlecht);
    }

    ngOnInit() {     
    }

    public Save() {
        this.FormGroup.markAllAsTouched();
        if(this.FormGroup.invalid) return;

        var description = new Description();
        description.Alter  = this.alterControl.value;
        description.Anrede = this.anredeControl.value;
        description.Augenfarbe = this.augenControl.value;
        description.Familienstatus = this.famlienstatusControl.value;
        description.Geschlecht = this.geschlechtControl.value;
        description.Gewicht = this.gewichtControl.value;
        description.Glaube = this.glaubeControl.value;
        description.Groesse = this.groesseControl.value;
        description.Haarfarbe = this.haarfarbeControl.value;
        description.Hautfarbe = this.hautfarbeControl.value;
        description.Kultur = this.kulurControl.value;
        description.Name = this.nameControl.value;
        description.Profession = this.professionControl.value;
        description.Rasse = this.rasseControl.value;

        this.dialogRef.close(description);
    }
    public Cancel() {
        this.dialogRef.close();
    }
}