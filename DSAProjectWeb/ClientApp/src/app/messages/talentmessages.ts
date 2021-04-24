import { Talent, TalentTypeEnum } from "../types/types";
import { DataMessage, ListMessage, Message } from "./baseMessages";

export class GetTalentListMessage extends ListMessage<Talent>{
  public Uri: string = 'Talent/GetList';
  public CharakterID: Number;
  public TalentType: TalentTypeEnum;
}

export class GetTalentViewMessage extends Message<Talent>{
  public Uri: string = 'Talent/GetViewList';
  public CharakterID: Number;
  public TalentGuid: number;
}

export class GetTalentViewListMessage extends ListMessage<Talent>{
  public Uri: string = 'Talent/GetViewList';
  public CharakterID: Number;
  public TalentType: TalentTypeEnum;
}

export class SetTalentMessage extends DataMessage<Talent>{
  public Uri: string = 'Talent/Set';
  public CharakterID: Number;
}
