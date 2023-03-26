import {makeAutoObservable} from "mobx";

export class GuessGameSettings {
    constructor() {
        makeAutoObservable(this);
        this.attempts = 6;
        this.wordLength = 5;
        this.language = "ru-RU";
    }

    public attempts: number;
    public wordLength: number;
    public language: string;

    setAttempts(attempts: number) : void {
        this.attempts = attempts;
    }

    setWordLength(wordLength: number) : void {
        this.wordLength = wordLength;
    }

    setLanguage(lang: string) : void {
        this.language = lang;
    }

    static fromJson(json: any): GuessGameSettings {
        const settings = new GuessGameSettings();
        settings.wordLength = json.wordLength;
        settings.attempts = json.attempts;
        settings.language = json.language;
        return settings;
    }
}