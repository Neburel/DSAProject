import { Injectable } from '@angular/core';
import { GetDescriptionMessage, SetDescriptionMessage } from 'src/app/messages/messages';
import { Charakter, Description } from 'src/app/types/types';
import { DialogService } from '../base/dialog.service';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';

@Injectable({
    providedIn: 'root'
})
export class DescriptionService extends Service {
    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetDescription(charakter: Charakter): Promise<Description> {
        var message = new GetDescriptionMessage();
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }
    public SetDescription(charakter: Charakter, description: Description): Promise<Description[]> {
        var message = new SetDescriptionMessage();
        message.Data = description;
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }
}