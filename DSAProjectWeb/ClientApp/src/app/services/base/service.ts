import { WebCommunicationService } from './web-communication.service';
import { DialogService } from './dialog.service';
import { Model } from 'src/app/types/types'
import { ListMessage, Message } from 'src/app/messages/messages';

export abstract class Service {

    constructor(public webCommunicationService: WebCommunicationService, protected dialog: DialogService) {
    }
    public sendMessage<T extends Model | void>(message: Message<T>, errorDialog: boolean = true): Promise<T> {
        return new Promise<T>((resolve, reject) => {
            this.webCommunicationService.sendMessage<T>(message).then(model => resolve(model)).catch(error => {
                if (errorDialog) { this.dialog.showMessageDialog('Fehler', error).subscribe(() => reject(error));
                } else{ reject(error); }
            });
        });
    }

    public sendListMessage<T extends Model>(message: ListMessage<T>, errorDialog: boolean = true): Promise<T[]> {
        return new Promise<T[]>((resolve, reject) => {
            this.webCommunicationService.sendListMessage<T>(message).then(list => resolve(list)).catch(error => {
                if (errorDialog) { this.dialog.showMessageDialog('Fehler', error).subscribe(() => reject(error));
                } else{ reject(error); }
            });
        });
    }
}
