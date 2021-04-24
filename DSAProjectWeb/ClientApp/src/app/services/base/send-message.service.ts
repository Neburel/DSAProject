import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Message } from 'src/app/messages/messages';

@Injectable({
  providedIn: 'root'
})
export class SendMessageService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public sendMessage(message: Message<any>): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      var uri = message.Uri;
      var contentType = message.ContentType;
      var apiUri = message.apiUri;

      if (!contentType) contentType = 'application/json;charset=UTF-8';
      delete message.Uri;
      delete message.ContentType;
      delete message.UseToken;
      delete message.apiUri;

      if (!apiUri) {
        apiUri = this.baseUrl;
      }

      var headers = new HttpHeaders({
        'Content-Type': contentType,
      });

      var options: any = {
        headers: headers
      }

      var completeUri = apiUri;
      if (uri) {
        completeUri = apiUri + uri
      }

      this.http.post<Response>(completeUri, message, options).subscribe((response: any) => {
        if (!response) reject('empty response');

        if (response.ResultCode == 0) {
          resolve(response.Data);
        } else {
          if (response.Data) {
            reject(response.Data.Message);
          }
          else {
            reject(response);
          }
        }
      }
        , error => {
          if (error.error) {
            reject(error.error.Message);
          }
          else {
            reject(error);
          }
        });
    });
  }
}
