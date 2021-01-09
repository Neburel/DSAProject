import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { Charakter } from 'src/app/types';
import * as fileSaver from 'file-saver';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
/** loading component*/
export class LoadingComponent implements OnInit {
  /** loading ctor */
  constructor(private charakterService: CharakterService) { }
  @ViewChild('fileChooser') FileChooser: ElementRef;
  @Output() Choosed: EventEmitter<Charakter> = new EventEmitter();
  public Loading: boolean = true;
  public CharakterList: Charakter[];

  ngOnInit(): void {
    this.Load();
  }

  private Load() {
    this.charakterService.GetList().then(result => {
      this.CharakterList = result;
      this.Loading = false;
    })
  }

  onSelect(charakter: Charakter): void {
    if (charakter == null) {
      this.charakterService.CreateCharakter().then(result => {
        this.emitEvent(result);
      });
    }
    else {
      this.emitEvent(charakter);
    };
  }

  public onImport(files: File[]) {
    let fileReader = new FileReader();
    var file = files[0] as File;
    if (file == null) return;
    this.FileChooser.nativeElement.value = '';

    fileReader.onload = (e => {
      var jsonObject = JSON.parse(fileReader.result as string)
      this.charakterService.ImportCharakter(fileReader.result).then(result => {
        console.log(result);
        this.emitEvent(result);
      });
    })
    fileReader.readAsText(file);
  }

  private emitEvent(charakter: Charakter) {
    this.Choosed.emit(charakter);
  }

  public export() {
    this.CharakterList.forEach(element => {
      this.charakterService.Export(element).then(result => {
        var data = new Blob([result], { type: 'text/plain;charset=utf-8' });
        fileSaver.saveAs(data, element.Name + '_export_' + new Date().getTime() + ".save");
      });
    });
  }
}

