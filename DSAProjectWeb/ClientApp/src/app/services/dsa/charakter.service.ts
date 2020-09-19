import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Injectable } from '@angular/core';
import { CreateCharakterMessage, GetCharakterListMesssage, Charakter } from 'src/app/types';

@Injectable({
    providedIn: 'root'
  })
export class CharakterService extends Service {
    public CurrentCharakter: Charakter;

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetList(): Promise<Charakter[]> {
        var message = new GetCharakterListMesssage();
        return this.sendListMessage<Charakter>(message);
    }

    public SetCurrentCharakter(charakter: Charakter): Promise<Charakter>{
        return new Promise(resolve =>{
            this.CurrentCharakter = charakter;
            resolve();
        })
    }

    public CreateCharakter(): Promise<void>{
        var message = new CreateCharakterMessage();
        return this.sendMessage(message);
    }
}
