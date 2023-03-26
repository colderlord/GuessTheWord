import {GuessGameSettings} from "./guessGameSettings";
import {makeAutoObservable} from "mobx";
import {HistoryItem, IHistoryItem} from "./historyItem.interface";

export interface IGuessGame {
    id: string
    endDate?: Date
    startDate?: Date
    creationDate: Date
    history: IHistoryItem[]
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
        this.history = [];
    }

    id: string;
    creationDate: Date;
    endDate?: Date;
    startDate?: Date;
    history: IHistoryItem[];

    setStartDate(startDate: Date) {
        this.startDate = startDate;
    }

    static fromJson(json: any): GuessGame {
        const settings = GuessGameSettings.fromJson(json.settings);
        const game = new GuessGame(settings);
        game.id = json.id;
        game.creationDate = json.creationDate;
        game.endDate = json.endDate;
        game.startDate = json.startDate;
        const history = json.history as [];
        for (const hi of history) {
            game.history.push(HistoryItem.fromJson(hi));
        }
        return game;
    }
}