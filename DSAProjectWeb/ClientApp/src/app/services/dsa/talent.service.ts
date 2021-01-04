import { Injectable } from '@angular/core';
import { Service } from '../base/service';
import { WebCommunicationService } from '../base/web-communication.service';
import { DialogService } from '../base/dialog.service';
import * as XLSX from 'xlsx';
import { GetLanguageListMessage, GetTalentViewListMessage, ImportTalentMessage, SetLanguageMessage, SetTalentMessage, GetTalentListMessage } from 'src/app/messages';
import { Charakter, Language, Talent, TalentTypeEnum } from 'src/app/types';

@Injectable({
    providedIn: 'root'
})
export class TalentService extends Service {
    private sheetNameWeaponless: string = 'Weaponless';
    private sheetNameClose: string = 'Close';
    private sheetNameRange: string = 'Range';
    private sheetNameLanguage: string = 'Language';
    private sheetNamePhysical: string = 'Physical';
    private sheetNameSocial: string = 'Social';
    private sheetNameNature: string = 'Nature';
    private sheetNameKnowldage: string = 'Knowldage';
    private sheetNameCrafting: string = 'Crafting';

    constructor(webCommunicationService: WebCommunicationService, dialog: DialogService) {
        super(webCommunicationService, dialog);
    }

    public GetTalentList(charakter: Charakter, talentType: TalentTypeEnum): Promise<Talent[]> {
        var message = new GetTalentListMessage();
        message.CharakterID = charakter.Id;
        message.TalentType = talentType;

        return this.sendListMessage(message);
    }

    public GetTalentViewList(charakter: Charakter, talentType: TalentTypeEnum): Promise<Talent[]> {
        var message = new GetTalentViewListMessage();
        message.CharakterID = charakter.Id;
        message.TalentType = talentType;

        return this.sendListMessage(message);
    }
    public GetLanguageViewList(charakter: Charakter): Promise<Language[]> {
        var message = new GetLanguageListMessage();
        message.CharakterID = charakter.Id;

        return this.sendMessage(message);
    }

    public SetTalent(charakter: Charakter, talent: Talent): Promise<Talent> {
        var message = new SetTalentMessage();
        message.CharakterID = charakter.Id;
        message.Data = talent;
        return this.sendMessage(message);
    }
    public SetLanguage(charakter: Charakter, talent: Language): Promise<Language> {
        var message = new SetLanguageMessage();
        message.CharakterID = charakter.Id;
        message.Data = talent;
        return this.sendMessage(message);
    }

    public Import(file: File) {
        return new Promise<void>((resolve, reject) => {
            var fileReader = new FileReader();
            fileReader.readAsArrayBuffer(file);

            fileReader.onload = (e) => {
                var arrayBuffer = fileReader.result as ArrayBuffer;
                var data = new Uint8Array(arrayBuffer);
                var arr = new Array();
                for (var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
                var bstr = arr.join("");
                var workbook = XLSX.read(bstr, { type: "binary" });

                var weaponlessPromise = this.importSheet(this.sheetNameWeaponless, workbook);
                var closePromise = this.importSheet(this.sheetNameClose, workbook);
                var rangePromise = this.importSheet(this.sheetNameRange, workbook);
                var languagePromise = this.importSheet(this.sheetNameLanguage, workbook);
                var physicalPromise = this.importSheet(this.sheetNamePhysical, workbook);
                var socialPromise = this.importSheet(this.sheetNameSocial, workbook);
                var naturePromise = this.importSheet(this.sheetNameNature, workbook);
                var knowldagePromise = this.importSheet(this.sheetNameKnowldage, workbook);
                var craftingPromise = this.importSheet(this.sheetNameCrafting, workbook);

                Promise.all([weaponlessPromise, closePromise, rangePromise, languagePromise, physicalPromise, socialPromise, naturePromise, knowldagePromise, craftingPromise]).then(result => {
                    var message = new ImportTalentMessage();
                    message.Weaponless = result[0];
                    message.Close = result[1];
                    message.Range = result[2];
                    message.Language = result[3];
                    message.Physical = result[4];
                    message.Social = result[5];
                    message.Nature = result[6];
                    message.Knowldage = result[7];
                    message.Crafting = result[8];


                    this.sendMessage(message).then(result => {
                        resolve();
                    }).catch(error => {
                        reject();
                    });
                }).catch(error => {
                    reject(error);
                });
            }
        });
    }
    private importSheet(sheetName: string, workbook: XLSX.WorkBook): Promise<any[]> {
        return new Promise<any[]>((resolve, reject) => {
            var worksheet = workbook.Sheets[sheetName];
            var arraylist = XLSX.utils.sheet_to_json<any>(worksheet, { raw: true });
            var pos = 0;
            arraylist = arraylist.map(item => {
                item.OrginalPosition = ++pos;
                if (item.Komplex1) item.Komplex1 = "'" + item.Komplex1 + "'";
                if (item.Komplex2) item.Komplex2 = "'" + item.Komplex2 + "'";

                return item;
            });
            resolve(arraylist);
        });
    }


}