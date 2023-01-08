import {GuessGameSettings} from "./guessGameSettings";
import {makeAutoObservable} from "mobx";

export interface IGuessGame {
    id: string
    endDate?: Date
    startDate?: Date
    creationDate: Date
}

export class GuessGame implements IGuessGame {
    public settings: GuessGameSettings;

    constructor(settings: GuessGameSettings) {
        makeAutoObservable(this);
        this.settings = settings;
        this.id = '-1';
        this.creationDate = new Date();
        this.endDate = new Date();
        this.startDate = new Date();
    }

    id: string;
    creationDate: Date;
    endDate?: Date;
    startDate?: Date;

    setStartDate(startDate: Date) {
        this.startDate = startDate;
    }
}