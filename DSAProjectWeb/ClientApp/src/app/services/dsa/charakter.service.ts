import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Injectable } from '@angular/core';
import { Charakter } from 'src/app/types';
import { GetCharakterListMesssage, CreateCharakterMessage, SetCharakterMessage } from 'src/app/messages';

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

    public SetCurrentCharakter(charakter: Charakter): Promise<Charakter> {
        console.log(charakter);
        this.CurrentCharakter = charakter;
        var message = new SetCharakterMessage();
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }

    public CreateCharakter(): Promise<Charakter> {
        var message = new CreateCharakterMessage();
        return this.sendMessage<Charakter>(message);
    }
}
