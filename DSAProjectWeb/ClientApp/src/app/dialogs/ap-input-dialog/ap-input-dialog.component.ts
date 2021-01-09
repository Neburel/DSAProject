import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AP } from 'src/app/types';

export class ApInputDialogOptions {
    public Invest: boolean = false;
    public Minus: boolean = false;
    public AP: number;
    public ManuelAP: number;
}

@Component({
    selector: 'app-ap-input-dialog',
    templateUrl: './ap-input-dialog.component.html',
    styleUrls: ['./ap-input-dialog.component.scss']
})

/** ApInputDialog component*/
export class ApInputDialogComponent implements OnInit {
    public Title = "Investiere Abenteuerpunkte";
    public ap: AP;
    public apGainResult: number = 0;
    public apInvestResult: number = 0;

    public apGainPlusControl = new FormControl(''); 
    public apGainMinusControl = new FormControl(''); 
    public apInvestPlusControl = new FormControl(''); 
    public apInvestMinusControl = new FormControl(''); 

    public FormGroup = new FormGroup({
        apGainPlus: this.apGainPlusControl,
        apGainMinus: this.apGainMinusControl,
        apInvestPlus: this.apInvestPlusControl,
        apInvestMinus: this.apInvestMinusControl,
    });
    constructor(public dialogRef: MatDialogRef<ApInputDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: AP) {
    }

    ngOnInit() {
        this.Title = "Abenteuerpunkte";
        this.ap = this.data;

        this.apGainMinusControl.setValue(0);
        this.apGainPlusControl.setValue(0);

        this.apInvestPlusControl.setValue(0);
        this.apInvestMinusControl.setValue(0);

        this.apGainPlusControl.valueChanges.subscribe(() => this.apGainChanged());
        this.apGainMinusControl.valueChanges.subscribe(() => this.apGainChanged());

        this.apInvestPlusControl.valueChanges.subscribe(() => this.apInvestChanged());
        this.apInvestMinusControl.valueChanges.subscribe(() => this.apInvestChanged());

        this.apGainChanged();
        this.apInvestChanged();
    }

    public Save() {
        this.FormGroup.markAllAsTouched();
        if(this.FormGroup.invalid) return;

        var result = new AP();
        result.APGainHand = this.apGainResult;
        result.APInvestHand = this.apInvestResult;

        this.dialogRef.close(result);
    }
    public Cancel() {
        this.dialogRef.close();
    }

    private apGainChanged(){
        this.apGainResult = this.ap.APGainHand + this.apGainPlusControl.value - this.apGainMinusControl.value;
    }
    private apInvestChanged(){
        this.apInvestResult = this.ap.APInvestHand + this.apInvestPlusControl.value - this.apInvestMinusControl.value;
    }
}