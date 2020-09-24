import { BehaviorSubject, Observable } from "rxjs";

export abstract class Model {

}
export abstract class AKTMODMAX extends Model {
  public AKT: number;
  public MOD: number;
  public MAX: number;
  public Name: string;
}

export class Charakter extends Model {
  public Id: number;
  public Name: String;
  public Created: Date;
  public LastUsed: Date;
}

export class Attribut extends AKTMODMAX {
  public ID: number;
  public NameShort: string;
}
export class Resource extends AKTMODMAX {
}
export class Value extends AKTMODMAX {

}

export class AP {
  public APAKT: number;  
  public AP: number;  
  public APLeft: number;
  public APInvested: number;
  public Level: number;
}

export class DSADataSource<T extends Model> {
  /** Stream of data that is provided to the table. */
  data;

  public setData(attributList: T[]) {
    this.data = new BehaviorSubject<T[]>(attributList);
  }

  /** Connect function called by the table to retrieve one stream containing the data to render. */
  connect(): Observable<T[]> {
    return this.data;
  }

  disconnect() { }
}