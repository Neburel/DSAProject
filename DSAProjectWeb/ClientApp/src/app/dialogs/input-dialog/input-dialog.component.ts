import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export class InputDialogData {
    public title: string;
    public text: string;
    public placeholder: string;
}

@Component({
    selector: 'app-input-dialog',
    templateUrl: './input-dialog.component.html',
    styleUrls: ['./input-dialog.component.scss']
})
/** input-dialog component*/
export class InputDialogComponent {
    public input: string;

    constructor(public dialogRef: MatDialogRef<InputDialogComponent, string>, @Inject(MAT_DIALOG_DATA) public data: InputDialogData) { }

    public ngOnInit() { }

    public handleKeypress(event): void {
        if (event.code == 'Enter') this.dialogRef.close(this.input);
    }
}
