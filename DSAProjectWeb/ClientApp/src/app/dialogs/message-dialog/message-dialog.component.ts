import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export class MessageDialogData {
  public title: string;
  public text: string;
}

@Component({
  selector: 'app-message-dialog',
  templateUrl: './message-dialog.component.html',
  styleUrls: ['./message-dialog.component.scss']
})
/** message-dialog component*/
export class MessageDialogComponent implements OnInit {
  /** message-dialog ctor */
  constructor(public dialogRef: MatDialogRef<MessageDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: MessageDialogData) { }
  public ngOnInit(): void { };
}
