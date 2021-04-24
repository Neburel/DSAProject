import { Model } from "../types/types";

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