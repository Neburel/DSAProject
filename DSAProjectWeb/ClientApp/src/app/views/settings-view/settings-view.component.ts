import { Component, ViewChild, ElementRef } from '@angular/core';
import { CharakterService } from 'src/app/services/dsa/charakter.service';
import { TalentService } from 'src/app/services/dsa/talent.service';

@Component({
    selector: 'app-settings-view',
    templateUrl: './settings-view.component.html',
    styleUrls: ['./settings-view.component.scss']
})
/** SettingsView component*/
export class SettingsViewComponent {
    @ViewChild('fileChooser') FileChooser: ElementRef;

    constructor(private charakterService: CharakterService, private talentService: TalentService) {

    }

    public talentImportFileChooser() {
        this.FileChooser.nativeElement.click();
    }

    public talentImport(event) {
        var file = event.target.files[0] as File;
        if (file == null) return;
        this.FileChooser.nativeElement.value = '';

        this.talentService.Import(file).then(result =>{
        });
    }
}