import { Injectable } from '@angular/core';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { CharakterService } from './charakter.service';
import { Charakter, AP } from 'src/app/types';
import { GetAPMessage, SetAPMessage, SetAPInvestedMessage } from 'src/app/messages';

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
    public Set(charakter: Charakter, value: number) {
        var message = new SetAPMessage();
        message.CharakterID = charakter.Id;
        message.Value = value;
        return this.sendMessage(message);
    }
    public SetInvested(charakter: Charakter, value: number) {
        var message = new SetAPInvestedMessage();
        message.CharakterID = charakter.Id;
        message.Value = value;
        return this.sendMessage(message);
    }
}