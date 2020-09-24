import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Attribut, Charakter } from 'src/app/types';
import { GetAttributListMessage, SetAttributAkt } from 'src/app/messages';

@Injectable({
    providedIn: 'root'
})
export class AttributService extends Service {
    public subject = new Subject();

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
        this.subject.subscribe(next => {
            console.log("NEXT");
        })
    }

    public GetAttributList(charakter: Charakter): Promise<Attribut[]> {
        var message = new GetAttributListMessage();
        message.CharakterID = charakter.Id;
        return this.sendListMessage<Attribut>(message);
    }

    public SetAttributAkt(charakter: Charakter, attributID: number, value: number): Promise<Number> {
        console.log("SET Attribut");
        console.log(attributID);

        return new Promise<Number>((resolve, reject) => {
            var message = new SetAttributAkt();
            message.AttributID = attributID;
            message.CharakterID = charakter.Id;
            message.Value = value;

            this.sendMessage(message).then((result) => {
                this.subject.next();
                resolve(1);
            })
        });
    }

}
