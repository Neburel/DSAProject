import { Model, Message, ListMessage, TestMessage, Attribut, GetAttributListMessage, SetAttributAkt } from 'src/app/types';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })
export class AttributService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetAttributList(characterID: number): Promise<Attribut[]>{
        var message = new GetAttributListMessage();
        message.ID = characterID;
        return this.sendListMessage<Attribut>(message);
    }

    public SetAttributAkt(characterID: number, attributID: number, value: number): Promise<Number>{
        return new Promise<Number>((resolve, reject) =>{
            var message = new SetAttributAkt();
            message.ID = attributID;
            message.CharakterID = characterID;
            message.Value = value;

            this.sendMessage(message).then((result) =>{
                resolve(1);
            })
        });
    }

}
