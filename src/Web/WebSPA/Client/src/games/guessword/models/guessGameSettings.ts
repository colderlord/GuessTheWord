import {makeAutoObservable} from "mobx";

export class GuessGameSettings {
    constructor() {
        makeAutoObservable(this);
        this.Attemts = 6;
        this.WordLength = 5;
        this.Language = "ru-RU";
    }

    public Attemts: number;
    public WordLength: number;
    public Language: string;

    setAttemts(attemts: number) : void {
        this.Attemts = attemts;
    }

    setWordLength(wordLength: number) : void {
        this.WordLength = wordLength;
    }

    setLanguage(lang: string) : void {
        this.Language = lang;
    }
}