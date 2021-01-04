import { EventEmitter } from "@angular/core";
import { DBADataTableButton } from "../types";

export type CallbackGetDeletePromise<T> = (element: T) => Promise<void>;
export type DbaMatTableCallbackButton<T> = (selectedList: T[]) => void;
export type DbaMatTableCreateDataListCallback<T> = (item: T) => T;

export function CreateDBADataTableButton<T>(id: string, label: string = null, disabled: boolean = false, singleSelected = false, click: DbaMatTableCallbackButton<T> = null): DBADataTableButton {
    var button = new DBADataTableButton();
    button.id = id;
    button.disabled = disabled;
    button.singleSelected = singleSelected;

    if (label) {
        button.label = label;
    }
    else {
        button.label = id;
    }
    button.click = new EventEmitter<any>();
    button.click.subscribe((result) => {
        if (click) click(result);
    });

    return button;
}
export function AddDbaMatTableRecID<T>(dataList: any[], editCallback: DbaMatTableCreateDataListCallback<T> = null): T[] {
    if (dataList.length > 0) {
        const rows = [];

        dataList.forEach((element: T, index: number) => {
            element['recId'] = index + 1;
            if (editCallback) {
                element = editCallback(element);
            }
            rows.push(element)
        });
    }
    else {
        dataList = [];
    }
    return dataList;
}