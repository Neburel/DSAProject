import { EventEmitter } from '@angular/core';
import { BehaviorSubject, Observable } from "rxjs";

export enum TalentTypeEnum {
  crafting = 1,
  knowldage = 2,
  nature = 3,
  physical = 4,
  social = 5,
  close = 6,
  range = 7,
  weaponless = 8,
  general = 9,
  language = 10,
}

export enum TraitTypeEnum {
  Keiner = 1,
  Vorteil = 2,
  Nachteil = 3,
  Event = 4,
  Belohnung = 5,
  Quest = 6,
  Geburstag = 7,
  Title = 8,
  Training = 9,
  Errungenschaft = 10,
  Buch = 11
}

export enum GeschlechtEnum {
  Weiblich = 1,
  Mänlich = 0,
  Geschlechtslos = 2
}
export enum FamilienstatusEnum {
  Ledig = 0,
  Verheiratet = 1,
  Geschieden = 2,
  Verwitwet = 3,
  Undefiniert = 4
}

export enum GeschlechtEnum {

}

export enum FamilienstatusEnum {

}

export interface GenericDataTableColumn {
  id: string;                 //Zugriff auf das Property Feld des Datentypes
  renderID?: string;          //ID die Benötigt wird, wenn für den Import ein zusätzliches Feld angelegt wird
  visible?: boolean           //Automatisch genutztes Feld, nicht selbst setzen
  hideColumn?: boolean;       //Feld nicht in der Tabelle anzeigen
  label: string,              //Anzeige (wird übersetzt) für die Tabelle und den Export
  hideOrder: number,          //Gibt an in welcher Reihenfolge Elemente in den Plus Button Verschwinden
  width?: number,             //Weite die Genutzt wird
  highlighted?: boolean;      //Automatisch genutztes Feld, nicht selbst setzen
  hovered?: boolean;          //Automatisch genutztes Feld, nicht selbst setzen
  datatype?: string;          //wird verwendet um z.B. Bilder abzuzeigen
  noexport?: boolean;         //Wenn gesetzt wird das angegebene Element nicht exportiert
  selectionListID?: string;
  selectedValueID?: string;
  nullCheckID?: string;

  clickEvent?: (column: any, input: any) => void;
  exportFunction?: (input: any) => string; //Aktion die Vor dem Export durchgeführt werden soll;
  //render?: (input: any) => string;
}

export class DBADataTableButton {
  id: string;
  disabled: boolean;
  singleSelected: boolean;
  label: string;
  click: EventEmitter<any> = new EventEmitter();
}

export class DBADataTableSelect {
  id: string;
  label: string;
}

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
  public APInvested: number;
  public APInvestHand: number;
  public APInvestTrait: number;
  public APGain: number;
  public APGainHand: number;
  public APGainTrait: number;
  public APLeft: number;
  public Level: number;
}

export class Money {
  public BankDublonen: number;
  public Dublonen: number;
  public Heller: number;
  public Kupfer: number;
  public Silber: number;
}

export class Talent {
  public ID: number;
  public TAW: number;
  public AT: number;
  public PA: number;
  public BL: number;

  public BE: string;
  public Name: string;
  public NameExtension: string;
  public Probe: string;
  public ProbeString: string;
  public Spezialisierung: string;
  public Anforderung: string;
  public DeductionText: TextView;
  public RequirementText: TextView;
  public DeductionSelected: Deduction;
  public DeductionList: Deduction[];
}

export class Language {
  public TawSprache: number;
  public TawSchrift: number;
  public IsTitle: boolean;
  public Mother: boolean;
  public IDSprache: string;
  public IDSchrift: string;
  public Sprache: string;
  public Schrift: string;
  public ProbeSprache: string;
  public ProbeSchrift: string;
  public KomplexSprache: string;
  public KomplexSchrift: string;
}

export class TextView {
  public Text: string;
  public FreeText: string;
}

export class Deduction {
  public ID: string;
  public Name: string;
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

export class IDValueView<T>{
  public Name: string;
  public ID: T;
  public Value: number;
}

export class Trait {
  public ID: number;
  public Name: string;
  public LongDescription: string;
  public Description: string;
  public Type: TraitTypeEnum;
  public APInvest: number;
  public APGain: number
  public Value: string;
  public GP: string;

  public ValueList: IDValueView<string>[];
  public TalentList: Talent[];
  public AttributList: IDValueView<number>[];
  public ResourceList: IDValueView<string>[];

  public ModifyDate: Date;
  public CreationDate: Date;
}

export class TalentTraitChoises {
  public Taw: Talent;
  public AT: Talent;
  public PA: Talent;
  public BL: Talent;
}

export class Description {
  public Name: string;
  public Anrede: string;
  public Rasse: string;
  public Kultur: string;
  public Profession: string;
  public Alter: number;
  public Geburstag: string;
  public Geschlecht: GeschlechtEnum;
  public Familienstatus: FamilienstatusEnum;
  public Hautfarbe: string;
  public Haarfarbe: string;
  public Augenfarbe: string;
  public Glaube: string;
  public Groesse: number;
  public Gewicht: number;
}