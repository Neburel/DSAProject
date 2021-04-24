import { Injectable } from '@angular/core';
import { GetNewTraitMessage, GetTraitGetTraitChoisesMessage as GetTraitChoisesMessage, GetTraitMessage, SetTraitMessage } from 'src/app/messages/messages';
import { Charakter, Trait } from 'src/app/types/types';
import { DialogService } from '../base/dialog.service';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';

@Injectable({
    providedIn: 'root'
})
export class TraitService extends Service {
    constructor(
        dialogService: DialogService,
        webCommunicationService: WebCommunicationService) {
        super(webCommunicationService, dialogService)
    }

    public GetNew(charakter: Charakter): Promise<Trait> {
        var message = new GetNewTraitMessage();
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }

    public GetList(charakter: Charakter): Promise<Trait[]> {
        var message = new GetTraitMessage();
        message.CharakterID = charakter.Id;
        return this.sendListMessage(message);
    }

    public Set(charakter: Charakter, data: Trait): Promise<void> {
        var message = new SetTraitMessage();
        message.CharakterID = charakter.Id;
        message.Data = data;
        return this.sendMessage(message);
    }
}
