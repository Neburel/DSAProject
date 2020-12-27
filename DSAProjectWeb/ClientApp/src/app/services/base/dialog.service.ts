import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { InputDialogComponent, InputDialogData } from 'src/app/dialogs/input-dialog/input-dialog.component';
import { OkCancelDialogComponent, OkCancelDialogData } from 'src/app/dialogs/ok-cancel-dialog/ok-cancel-dialog.component';
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

  public showInputDialog(title: string, text: string, placeholder: string): Observable<string> {
    var data = new InputDialogData();
    data.title = title;
    data.text = text;
    data.placeholder = placeholder;
    return this.dialog.open(InputDialogComponent, { data: data }).afterClosed();
}

public showOkCancelDialog(title: string, text: string): Observable<boolean> {
    var data = new OkCancelDialogData();
    data.title = title;
    data.text = text;
    return this.dialog.open(OkCancelDialogComponent, { data: data }).afterClosed();
}
}
