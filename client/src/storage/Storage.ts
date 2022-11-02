import { makeAutoObservable } from "mobx"
import { Letter, LetterType } from "../interfaces/Letter";
import { Word } from '../interfaces/Word'
import { Settings } from '../interfaces/Settings'

export class Storage {
    constructor() {
        makeAutoObservable(this);
        this.Settings = new SettingsModel();
    }

    Settings: Settings;
    WordModel: Word[] = [];
    AddWord(value: string) {
        const val = value.toLowerCase();
        const chars = value.toLowerCase().split('');
        const word = new WordModel();
        word.stringValue = val;
        chars.map((v, index) => {
            const letter = new LetterModel();
            letter.letter = v;
            letter.letterType = LetterType.None;
            letter.position = index;
            word.letters.push(letter);
        });
        this.WordModel.push(word);
    }
    Clear() {
        this.WordModel = [];
    }
}

export class SettingsModel implements Settings {
    constructor() {
        makeAutoObservable(this);
    }

    SetAttemptsCount(value: number): void {
        this.Attempts = value;
    }

    SetLettersCount(value: number) : void {
        this.LettersCount = value;
    }

    SetCulture(value: string) : void {
        this.Culture = value;
    }

    Culture = "ru-RU";
    LettersCount = 5;
    Attempts = 6;
}

export class WordModel implements Word {
    constructor() {
        makeAutoObservable(this);
    }

    stringValue: string = "";

    letters: Letter[] = [];
}

export class LetterModel implements Letter {
    constructor() {
        makeAutoObservable(this);
    }

    letter = "";
    letterType = LetterType.Default;
    position = -1;
}