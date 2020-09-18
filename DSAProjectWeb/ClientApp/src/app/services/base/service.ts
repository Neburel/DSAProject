import { Observable } from 'rxjs';
import { WebCommunicationService } from './web-communication.service';
import { DialogService } from './dialog.service';
import { Model, Message, ListMessage } from 'src/app/types';


export abstract class Service {

    constructor(public webCommunicationService: WebCommunicationService, protected dialog: DialogService) {
    }
    public sendMessage<T extends Model | void>(message: Message<T>, errorDialog: boolean = true): Promise<T> {
        return new Promise<T>((resolve, reject) => {
            this.webCommunicationService.sendMessage(message).then(model => resolve(model)).catch(error => {
                if (errorDialog) { this.dialog.showMessageDialog('Fehler', error).subscribe(() => reject(error));
                } else{ reject(error); }
            });
        });
    }

    public sendListMessage<T extends Model>(message: ListMessage<T>, errorDialog: boolean = true): Promise<T[]> {
        return new Promise<T[]>((resolve, reject) => {
            this.webCommunicationService.sendListMessage(message).then(list => resolve(list)).catch(error => {
                if (errorDialog) { this.dialog.showMessageDialog('Fehler', error).subscribe(() => reject(error));
                } else{ reject(error); }
            });
        });
    }
}
