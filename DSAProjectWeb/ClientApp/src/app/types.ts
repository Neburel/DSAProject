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
  public APAKT: number;
  public AP: number;
  public APLeft: number;
  public APInvested: number;
  public Level: number;
}

export class Talent {
  public TAW: number;
  public AT: number;
  public PA: number;
  public BL: number;

  public BE: string;
  public Name: string;
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