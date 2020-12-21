import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';

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
    private inputAbenteuerPunkte: FormControl = new FormControl('', [Validators.required, Validators.pattern("^[0-9]*$")]);

    public FormGroup = new FormGroup({
        inputAbenteuerPunkte: this.inputAbenteuerPunkte
    });

    constructor(public dialogRef: MatDialogRef<ApInputDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: ApInputDialogOptions) {
        console.log(data);
    }

    ngOnInit() {
        if (this.data.Invest) {
            if (this.data.Minus) {
                this.Title = "Reduziere Investierte Abenteuerpunte";
            }
            else {
                this.Title = "Investiere Abenteuerpunkte";
            }
        }
        else {
            if (this.data.Minus) {
                this.Title = "Reduziere Verdiente Abenteuerpunte";
            }
            else {
                this.Title = "Verdiene Abenteuerpunkte";
            }
        }        
    }

    public Save() {
        this.FormGroup.markAllAsTouched();
        if(this.FormGroup.invalid) return;
        this.dialogRef.close(this.inputAbenteuerPunkte.value);
    }
    public Cancel() {
        this.dialogRef.close();
    }
}