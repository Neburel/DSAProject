import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export class OkCancelDialogData {
    public title: string;
    public text: string;
}

@Component({
    selector: 'app-ok-cancel-dialog',
    templateUrl: './ok-cancel-dialog.component.html',
    styleUrls: ['./ok-cancel-dialog.component.scss']
})
/** ok-cancel-dialog component*/
export class OkCancelDialogComponent {
    /** ok-cancel-dialog ctor */
    constructor(public dialogRef: MatDialogRef<OkCancelDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: OkCancelDialogData) { }

    public ngOnInit(): void { 
    }

    public ok(): void {
        this.dialogRef.close(true);
    }

    public cancel(): void {
        this.dialogRef.close(false);
    }
}
