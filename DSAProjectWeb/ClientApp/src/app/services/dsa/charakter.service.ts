import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Injectable } from '@angular/core';
import { CreateCharakterMessage } from 'src/app/types';

@Injectable({
    providedIn: 'root'
  })
export class CharakterService extends Service {

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public CreateCharakter(): Promise<void>{
        var message = new CreateCharakterMessage();
        return this.sendMessage(message);
    }
}
