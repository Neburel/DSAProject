import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Injectable } from '@angular/core';
import { Charakter } from 'src/app/types/types';
import { GetCharakterListMesssage, CreateCharakterMessage, SetCharakterMessage, ImportCharakterMessage, ExportCharakterMessage, DeleteCharakterMessage } from 'src/app/messages/messages';

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

    public SetCharakter(charakter: Charakter): Promise<Charakter> {
        this.CurrentCharakter = charakter;
        var message = new SetCharakterMessage();
        message.CharakterID = charakter.Id;
        message.Data = charakter;
        return this.sendMessage(message);
    }

    public DeleteCharakter(charakter: Charakter): Promise<Charakter[]> {
        var message = new DeleteCharakterMessage();
        message.CharakterID = charakter.Id;
        return this.sendListMessage(message);
    }

    public CreateCharakter(): Promise<Charakter> {
        var message = new CreateCharakterMessage();
        return this.sendMessage<Charakter>(message);
    }

    public ImportCharakter(data: any): Promise<Charakter> {
        var message = new ImportCharakterMessage();
        message.Data = data;
        return this.sendMessage(message);
    }

    public Export(charakter: Charakter): Promise<string> {
        var message = new ExportCharakterMessage();
        message.CharakterID = charakter.Id;

        return this.sendMessage(message);
    }
}
