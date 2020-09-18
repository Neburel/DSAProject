import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { MessageDialogData, MessageDialogComponent } from '../../dialogs/message-dialog/message-dialog.component';

@Injectable({
  providedIn: 'root'  
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  public showMessageDialog(title: string, text: string): Observable<void> {
    var data = new MessageDialogData();
    data.title = title;
    data.text = text;
    return this.dialog.open(MessageDialogComponent, { data: data }).afterClosed();
  }
}
