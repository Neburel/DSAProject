import { Component } from '@angular/core';

export interface Transaction {
  item: string;
  cost: number;
  akt: number;
  mod: number;
  max: number;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  displayedColumns: string[] = ['item', 'akt', 'mod', 'cost'];
  transactions: Transaction[] = [
    { item: 'Mut', cost: 4, akt: 4, mod: 3, max: 2 },
    { item: 'Klugheit', cost: 5, akt: 4, mod: 3, max: 2 },
    { item: 'Intuition', cost: 2, akt: 4, mod: 3, max: 2 },
    { item: 'Charisma', cost: 4, akt: 4, mod: 3, max: 2 },
    { item: 'Fingerfertigkeit', cost: 25, akt: 4, mod: 3, max: 2 },
    { item: 'Gewandheit', cost: 15, akt: 4, mod: 3, max: 2 },
    { item: 'Konstitution', cost: 15, akt: 4, mod: 3, max: 2 },
    { item: 'Körperkraft', cost: 15, akt: 4, mod: 3, max: 2 },
    { item: 'Sozialstatus', cost: 15, akt: 4, mod: 3, max: 2 },
  ];

  /** Gets the total cost of all transactions. */
  getTotalCost() {
    return this.transactions.map(t => t.cost).reduce((acc, value) => acc + value, 0);
  }
}
