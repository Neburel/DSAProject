import { Component, OnInit } from '@angular/core';
import { ApService } from 'src/app/services/dsa/ap.service';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { AP } from 'src/app/types';
import { MatDialogRef, MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ApInputDialogComponent, ApInputDialogOptions } from 'src/app/dialogs/ap-input-dialog/ap-input-dialog.component';

@Component({
    selector: 'app-adventure-tabel',
    templateUrl: './adventure-tabel.component.html',
    styleUrls: ['./adventure-tabel.component.scss']
})
/** adventureTabel component*/
export class AdventureTabelComponent implements OnInit {
    public AP: AP = new AP();

    constructor(private charakterService: CharakterService, private apService: ApService, private dialog: MatDialog) { }

    ngOnInit(): void {
        this.Load();
    }

    private Load() {
        this.apService.Get(this.charakterService.CurrentCharakter).then(result => {
            this.AP = result;
            console.log(result);
        })
    }
    public InvestApClick() {
        var inputData = new ApInputDialogOptions();
        inputData.Invest = false;
        inputData.ManuelAP = 1000;
        inputData.AP = 10000;
        console.log(inputData);

        var config = new MatDialogConfig();
        config.data = inputData;

        let dialogRef = this.dialog.open(ApInputDialogComponent, config);
        var subscribtion = dialogRef.afterClosed().subscribe(next => {
            console.log(next);
            subscribtion.unsubscribe();
        });
    }
}