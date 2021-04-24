import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Money } from 'src/app/types/types';

@Component({
    selector: 'app-bank-dialog',
    templateUrl: './bank-dialog.component.html',
    styleUrls: ['./bank-dialog.component.scss']
})
/** BankDialog component*/
export class BankDialogComponent implements OnInit {
    public Title = "Bank";
    public money: Money;

    public resultHeller: number = 0;
    public resultKupfer: number = 0;
    public resultSilber: number = 0;
    public resultDublonen: number = 0;
    public resultBank: number = 0;

    public hellerPlusControl = new FormControl('');
    public hellerMinusControl = new FormControl('');
    public kupferPlusControl = new FormControl('');
    public kupferMinusControl = new FormControl('');
    public silberPlusControl = new FormControl('');
    public silberMinusControl = new FormControl('');
    public dublonenPlusControl = new FormControl('');
    public dublonenMinusControl = new FormControl('');
    public bankPlusControl = new FormControl('');
    public bankMinusControl = new FormControl('');

    public FormGroup = new FormGroup({
        hellerPlus: this.hellerPlusControl,
        hellerMinus: this.hellerMinusControl,
        kupferPlus: this.kupferPlusControl,
        kupferMinus: this.kupferMinusControl,
        silberPlus: this.silberPlusControl,
        silberMinus: this.silberMinusControl,
        dublonenPlus: this.dublonenPlusControl,
        dublonenMinus: this.dublonenMinusControl,
        bankPlus: this.bankPlusControl,
        bankMinus: this.bankMinusControl
    });
    /** BankDialog ctor */
    constructor(
        public dialogRef: MatDialogRef<BankDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Money
    ) {

    }

    ngOnInit(): void {
       this.money = this.data;
       
       this.hellerPlusControl.setValue(0);
       this.hellerMinusControl.setValue(0);
       
       this.kupferPlusControl.setValue(0);
       this.kupferMinusControl.setValue(0);

       this.silberPlusControl.setValue(0);
       this.silberMinusControl.setValue(0);
       
       this.dublonenPlusControl.setValue(0);
       this.dublonenMinusControl.setValue(0);

       this.bankPlusControl.setValue(0);
       this.bankMinusControl.setValue(0);

       this.hellerPlusControl.valueChanges.subscribe(() => this.hellerChanged())
       this.hellerMinusControl.valueChanges.subscribe(() => this.hellerChanged())

       this.kupferPlusControl.valueChanges.subscribe(() => this.kupferChanged())
       this.kupferMinusControl.valueChanges.subscribe(() => this.kupferChanged())

       this.silberPlusControl.valueChanges.subscribe(() => this.silberChanged())
       this.silberMinusControl.valueChanges.subscribe(() => this.silberChanged())

       this.dublonenPlusControl.valueChanges.subscribe(() => this.dublonenChanged())
       this.dublonenMinusControl.valueChanges.subscribe(() => this.dublonenChanged())

       this.bankPlusControl.valueChanges.subscribe(() => this.bankChanged())
       this.bankMinusControl.valueChanges.subscribe(() => this.bankChanged())
    }


    public Save() {
        this.FormGroup.markAllAsTouched();
        if (this.FormGroup.invalid) return;

        var money = new Money();
        money.BankDublonen = this.resultBank;
        money.Dublonen = this.resultDublonen;
        money.Heller = this.resultHeller;
        money.Kupfer = this.resultKupfer;
        money.Silber = this.resultSilber;

        this.dialogRef.close(money);
    }
    public Cancel() {
        this.dialogRef.close();
    }

    private hellerChanged(){
        this.resultHeller = this.money.Heller + this.hellerPlusControl.value - this.hellerMinusControl.value;
    }
    private kupferChanged(){
        this.resultKupfer = this.money.Kupfer + this.kupferPlusControl.value - this.kupferMinusControl.value;
    }
    private silberChanged(){
        this.resultSilber = this.money.Silber + this.silberPlusControl.value - this.silberMinusControl.value;
    }
    private dublonenChanged(){
        this.resultDublonen = this.money.Dublonen + this.dublonenPlusControl.value - this.dublonenMinusControl.value;
    }
    private bankChanged(){
        this.resultBank = this.money.BankDublonen + this.bankPlusControl.value - this.bankMinusControl.value;
    }
}