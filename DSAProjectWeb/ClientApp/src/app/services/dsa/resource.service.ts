import { Injectable } from '@angular/core';
import { Resource, Charakter } from 'src/app/types';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { GetResourceListMessage } from 'src/app/messages';

@Injectable({
    providedIn: 'root'
})
export class ResourceService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetList(charakter: Charakter): Promise<Resource[]> {
        var message = new GetResourceListMessage();
        message.CharakterID = charakter.Id;
        return this.sendListMessage<Resource>(message);
    }
}