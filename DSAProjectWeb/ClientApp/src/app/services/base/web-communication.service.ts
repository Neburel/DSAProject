import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { SendMessageService } from './send-message.service';
import { Model } from 'src/app/types/types';
import { ListMessage, Message } from 'src/app/messages/messages';


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
