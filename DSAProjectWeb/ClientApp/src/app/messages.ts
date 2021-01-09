import { BehaviorSubject, Observable } from "rxjs";
import { Model, Charakter, Attribut, Value, AP, Talent, TalentTypeEnum, Language, Description, Money } from "./types";

export abstract class Message<T extends Model | void> {
  public abstract Uri: string;
  public ContentType: string;
  public apiUri: string;     //Alternative angabe zum Nutzen einer nicht durch die Config bestimmten API
  public UseToken: boolean = true;
}
export abstract class MessageCharakterID<T extends Model | void> extends Message<T>{
  public CharakterID: number;
}

export abstract class DataMessage<T extends Model> extends Message<T> {
  public Data: T;
  public ContentType: string = "application/json";
}

export abstract class ListMessage<T extends Model> extends Message<T> { }

export class CreateCharakterMessage extends Message<void> {
  public Uri: string = 'Charakter/Create';
}
export class ImportCharakterMessage extends DataMessage<any> {
  public Uri: string = 'Charakter/Import';
}
export class ExportCharakterMessage extends Message<string> {
  public Uri: string = 'Charakter/Export';
  public CharakterID: number;
}
export class SetCharakterMessage extends DataMessage<Charakter> {
  public Uri: string = 'Charakter/Set';
  public CharakterID: number;
}

export class GetCharakterListMesssage extends ListMessage<Charakter>{
  public Uri: string = 'Charakter/GetList';
}
export class SetCharakterMesssage extends Message<Charakter>{
  public Uri: string = 'Charakter/Set';
}

export class GetAttributListMessage extends ListMessage<Attribut>{
  public Uri: string = 'Attribut/GetList';
  public CharakterID: Number;
}
export class SetAttributAkt extends Message<void>{
  public Uri: string = 'Attribut/Set';
  public Value: Number;
  public CharakterID: Number;
  public AttributID: Number;
}

export class GetResourceListMessage extends ListMessage<Attribut>{
  public Uri: string = 'Resource/GetList';
  public CharakterID: Number;
}
export class GetValueListMessage extends ListMessage<Value>{
  public Uri: string = 'Value/GetList';
  public CharakterID: Number;
}

export class GetAPMessage extends MessageCharakterID<AP>{
  public Uri: string = 'AP/Get'
}
export class SetAPMessage extends DataMessage<AP>{
  public Uri: string = 'AP/Set'
  public CharakterID: Number;
}

export class GetTalentListMessage extends ListMessage<Talent>{
  public Uri: string = 'Talent/GetList';
  public CharakterID: Number;
  public TalentType: TalentTypeEnum;
}

export class GetTalentViewListMessage extends ListMessage<Talent>{
  public Uri: string = 'Talent/GetViewList';
  public CharakterID: Number;
  public TalentType: TalentTypeEnum;
}

export class GetLanguageListMessage extends ListMessage<Language>{
  public Uri: string = 'Talent/GetLanguageList';
  public CharakterID: Number;
}

export class SetTalentMessage extends DataMessage<Talent>{
  public Uri: string = 'Talent/Set';
  public CharakterID: Number;
}
export class SetLanguageMessage extends DataMessage<Language>{
  public Uri: string = 'Talent/SetLanguage';
  public CharakterID: Number;
}

export class GetNewTraitMessage extends Message<any>{
  public Uri: string = 'Trait/New';
  public CharakterID: Number;
}
export class GetTraitMessage extends ListMessage<any>{
  public Uri: string = 'Trait/GetList';
  public CharakterID: Number;
}
export class GetTraitGetTraitChoisesMessage extends ListMessage<any>{
  public Uri: string = 'Trait/GetTraitChoises';
  public CharakterID: Number;
}
export class SetTraitMessage extends DataMessage<any>{
  public Uri: string = 'Trait/Set';
  public CharakterID: Number;
}

export class ImportTalentMessage extends Message<void>{
  public Uri: string = 'Talent/Import';
  public Weaponless: any;
  public Close: any;
  public Range: any;
  public Language: any;
  public Physical: any;
  public Social: any;
  public Nature: any;
  public Knowldage: any;
  public Crafting: any;
}

export class GetDescriptionMessage extends Message<void>{
  public Uri: string = 'Description/Get';
  public CharakterID: Number;
}
export class SetDescriptionMessage extends DataMessage<Description>{
  public Uri: string = 'Description/Set';
  public CharakterID: Number;
}

export class GetMoneyMessage extends Message<void>{
  public Uri: string = 'Money/Get';
  public CharakterID: Number;
}
export class SetMoneyMessage extends DataMessage<Money>{
  public Uri: string = 'Money/Set';
  public CharakterID: Number;
}


export class TestMessage extends Message<void>{
  public Uri: string = 'Talent/Test';
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
