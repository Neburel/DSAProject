import { Injectable } from '@angular/core';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Value, GetValueListMessage } from 'src/app/types';

@Injectable({
    providedIn: 'root'
})
export class ValueService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetList(characterID: number): Promise<Value[]> {
        var message = new GetValueListMessage();
        message.ID = characterID;
        return this.sendListMessage<Value>(message);
    }
}