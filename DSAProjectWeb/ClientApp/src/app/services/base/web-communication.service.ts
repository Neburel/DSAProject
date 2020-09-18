import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEventType } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SendMessageService } from './send-message.service';
import { Model, Message, ListMessage } from 'src/app/types';


@Injectable({
  // we declare that this service should be created
  // by the root application injector.
  providedIn: 'root',
})
export class WebCommunicationService {

  constructor(
    private sendMessageService: SendMessageService,
    private http: HttpClient) {
  }

  public sendMessage<T extends Model | void>(message: Message<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      this.sendMessageInner(message).then((result) => {
        resolve(result);
      }, error => reject(error));
    });
  }

  public sendListMessage<T extends Model>(message: ListMessage<T>): Promise<T[]> {
    return new Promise<T[]>((resolve, reject) => {
      this.sendMessageInner(message).then((result) => {
        resolve(result);
      }).catch(error => {
        console.log(error);
        reject(error);
      });
    });
  }

  private sendMessageInner(message: Message<any>): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      this.sendMessageService.sendMessage(message).then((result) => {
        resolve(result);
      }, error => reject(error));
    });
  }
}
