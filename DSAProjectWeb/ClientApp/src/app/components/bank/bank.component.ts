import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BankDialogComponent } from 'src/app/dialogs/bank-dialog/bank-dialog.component';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { MoneyService } from 'src/app/services/dsa/money.service';
import { Money } from 'src/app/types';

@Component({
    selector: 'app-bank',
    templateUrl: './bank.component.html',
    styleUrls: ['./bank.component.scss']
})
/** bank component*/
export class BankComponent {
    public money: Money;
    public loading: boolean = true;
    /** bank ctor */
    constructor(
        private moneyService: MoneyService,
        private charakterService: CharakterService,
        private dialog: MatDialog) {

    }

    ngOnInit(): void {
        this.moneyService.GetMoney(this.charakterService.CurrentCharakter).then(result => {
            this.money = result;
            this.loading = false;
        })
    }

    public OpenDialog(): void {
        const dialogRef = this.dialog.open(BankDialogComponent, { data: this.money });
        var subscription = dialogRef.afterClosed().subscribe((money) => {
            if (money != null) {
                this.moneyService.SetMoney(this.charakterService.CurrentCharakter, money).then(result => {

                    this.money = money;
                    subscription.unsubscribe();

                })
            }
            else {
                subscription.unsubscribe();
            }
        });
    }
}