import { Injectable } from '@angular/core';
import { GetResourceListMessage, Resource } from 'src/app/types';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';

@Injectable({
    providedIn: 'root'
})
export class ResourceService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetList(characterID: number): Promise<Resource[]> {
        var message = new GetResourceListMessage();
        message.ID = characterID;
        return this.sendListMessage<Resource>(message);
    }
}