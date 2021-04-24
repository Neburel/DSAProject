import { Injectable } from '@angular/core';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Charakter, AP } from 'src/app/types/types';
import { GetAPMessage, SetAPMessage } from 'src/app/messages/messages';

@Injectable({
    providedIn: 'root'
})
export class ApService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public Get(charakter: Charakter): Promise<AP> {
        var message = new GetAPMessage();
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }
    public Set(charakter: Charakter, value: AP) {
        var message = new SetAPMessage();
        message.CharakterID = charakter.Id;
        message.Data = value;
        return this.sendMessage(message);
    }
}