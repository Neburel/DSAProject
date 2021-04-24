import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DescriptionDialogComponent } from 'src/app/dialogs/description-dialog/description-dialog.component';
import { DialogService } from 'src/app/services/base/dialog.service';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { DescriptionService } from 'src/app/services/dsa/description.service';
import { Description, FamilienstatusEnum, GeschlechtEnum } from 'src/app/types/types';

@Component({
    selector: 'app-description',
    templateUrl: './description.component.html',
    styleUrls: ['./description.component.scss']
})
/** Description component*/
export class DescriptionComponent implements OnInit {
    /** Description ctor */
    public description: Description;
    public loading: boolean = false;
    public geschlechtEnum = GeschlechtEnum;
    public familienstatusEnum = FamilienstatusEnum;
    constructor(
        private charakterService: CharakterService,
        private descriptionService: DescriptionService,
        private dialogService: DialogService,
        private dialog: MatDialog
    ) {

    }
    ngOnInit(): void {
        this.descriptionService.GetDescription(this.charakterService.CurrentCharakter).then(result => {
            this.description = result;
            this.loading = true;
        })
    }

    public OpenDialog(): void {
        const dialogRef = this.dialog.open(DescriptionDialogComponent, { data: this.description });
        var subscription = dialogRef.afterClosed().subscribe((newDescription) => {
            if (newDescription != null) {
                this.descriptionService.SetDescription(this.charakterService.CurrentCharakter, newDescription).then(result => {
                    this.description = newDescription;
                    subscription.unsubscribe();
                })
            }
            else{
                subscription.unsubscribe();
            }
        });
    }
}