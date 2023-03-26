import {makeAutoObservable} from "mobx";

export interface IHistoryItem {

}

export class HistoryItem implements IHistoryItem {
    constructor() {
        makeAutoObservable(this);
    }

    static fromJson(json: any) : HistoryItem {
        const historyItem = new HistoryItem();
        return historyItem;
    }
}