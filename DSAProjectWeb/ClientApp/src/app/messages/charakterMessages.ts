import { Charakter } from "../types/types";
import { DataMessage, Message } from "./baseMessages";

const CHARAKTERURIBASE = 'Charakter/'

export class CreateCharakterMessage extends Message<void> {
    public Uri: string = CHARAKTERURIBASE + 'Create';
}

export class ImportCharakterMessage extends DataMessage<any> {
    public Uri: string = CHARAKTERURIBASE + 'Import';
}

export class ExportCharakterMessage extends Message<string> {
    public Uri: string = CHARAKTERURIBASE + 'Export';
    public CharakterID: number;
}

export class SetCharakterMessage extends DataMessage<Charakter> {
    public Uri: string = CHARAKTERURIBASE + 'Set';
    public CharakterID: number;
}

export class DeleteCharakterMessage extends DataMessage<Charakter> {
    public Uri: string = CHARAKTERURIBASE + 'Delete';
    public CharakterID: number;
}
