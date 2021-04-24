import { Injectable } from '@angular/core';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Value, Charakter } from 'src/app/types/types';
import { GetValueListMessage } from 'src/app/messages/messages';

@Injectable({
    providedIn: 'root'
})
export class ValueService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetList(charakter: Charakter): Promise<Value[]> {
        var message = new GetValueListMessage();
        message.CharakterID = charakter.Id;
        return this.sendListMessage<Value>(message);
    }
}