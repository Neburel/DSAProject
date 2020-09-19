import { BehaviorSubject, Observable } from "rxjs";

export abstract class Model {
  public ID: number;
}
export abstract class AKTMODMAX extends Model {
  public AKT: number;
  public MOD: number;
  public MAX: number;
  public Name: string;
}

export class Charakter extends Model{

}

export class Attribut extends AKTMODMAX {
  public NameShort: string;
}
export class Resource extends AKTMODMAX {
}
export class Value extends AKTMODMAX{

}

export abstract class Message<T extends Model | void> {
  public abstract Uri: string;
  public ContentType: string;
  public apiUri: string;     //Alternative angabe zum Nutzen einer nicht durch die Config bestimmten API
  public UseToken: boolean = true;
}

export abstract class DataMessage<T extends Model> extends Message<T> {
  public Data: T;
  public ContentType: string = "application/json";
}

export abstract class ListMessage<T extends Model> extends Message<T> { }

export class TestMessage extends Message<void>{
  public Uri: string = 'Attribut/GetList';
}

export class CreateCharakterMessage extends Message<void>{
  public Uri: string = 'Charakter/Create';
}

export class GetCharakterListMesssage extends ListMessage<Charakter>{
  public Uri: string = 'Charakter/GetList';
}

export class GetAttributListMessage extends ListMessage<Attribut>{
  public Uri: string = 'Attribut/GetList';
  public ID: Number;
}
export class SetAttributAkt extends Message<void>{
  public Uri: string = 'Attribut/Set';
  public ID: Number;
  public CharakterID: Number;
  public Value: Number;
}

export class GetResourceListMessage extends ListMessage<Attribut>{
  public Uri: string = 'Resource/GetList';
  public ID: Number;
}
export class GetValueListMessage extends ListMessage<Value>{
  public Uri: string = 'Value/GetList';
  public ID: Number;
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