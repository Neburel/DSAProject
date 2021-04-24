import { Injectable } from '@angular/core';
import { from } from 'rxjs';
import { Charakter } from 'src/app/types/types';
import { DialogService } from '../base/dialog.service';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { Money } from '../../types/types';
import { GetMoneyMessage, SetMoneyMessage } from 'src/app/messages/messages';

@Injectable({
    providedIn: 'root'
})
export class MoneyService extends Service {
    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetMoney(charakter: Charakter): Promise<Money> {
        var message = new GetMoneyMessage();
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }
    public SetMoney(charakter: Charakter, description: Money): Promise<Money[]> {
        var message = new SetMoneyMessage();
        message.Data = description;
        message.CharakterID = charakter.Id;
        return this.sendMessage(message);
    }
}