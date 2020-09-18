import { Observable, forkJoin } from 'rxjs';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import { Service } from '../base/service';
import { Injectable } from '@angular/core';
import { CharakterService } from './charakter.service';

@Injectable({
    providedIn: 'root'
  })
export abstract class AppService extends Service {

    constructor(
        private charakterService: CharakterService,
        dialogService: DialogService,
        webCommunicationService: WebCommunicationService) {
        super(webCommunicationService, dialogService)
    }
    public Init(): Observable<void> {
        return new Observable((subscriber) => {
            var createCharakter = this.charakterService.CreateCharakter();

            forkJoin([createCharakter]).subscribe(result => {
                subscriber.next();
                subscriber.complete();
            }, error => {
                subscriber.error(error);
            }, () => {
                subscriber.complete();
            });
        });
    }

}
