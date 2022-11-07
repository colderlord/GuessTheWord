import {makeAutoObservable, runInAction} from "mobx"
import {Letter, LetterType} from "../interfaces/Letter";
import {Word} from '../interfaces/Word'
import {Settings} from '../interfaces/Settings'
import GuessTheWordService from "../services/GuessTheWordService";
import TryGuessGameModel from "./Games/TryGuessGameModel";
import {GameInfoStorage} from "./GameInfoStorage";
import {LangInfoStorage} from "./LangInfoStorage";

export class Storage {

    constructor() {
        makeAutoObservable(this);
        this.guessTheWordService = new GuessTheWordService();
        this.gameInfosStorage = new GameInfoStorage(this.guessTheWordService);
        this.langInfosStorage = new LangInfoStorage(this.guessTheWordService);
        this.settings = new SettingsModel();
        this.currentGameInfo = this.gameInfosStorage.defaultGameInfo;
        this.currentLangInfo = this.langInfosStorage.defaultLanguage;
    }

    guessTheWordService: GuessTheWordService;
    gameInfosStorage: GameInfoStorage;
    langInfosStorage: LangInfoStorage;

    currentGameInfo: GameInfo;
    currentLangInfo: LangInfo;
    settings: Settings;
    gameModel?: TryGuessGameModel = undefined;

    setCurrentGame(gameInfo: GameInfo) {
        this.currentGameInfo = gameInfo;
    }
    async setGameModel(uid: string) {
        await this.guessTheWordService.setRules(uid, this.settings);
        runInAction(() => {
            this.gameModel = new TryGuessGameModel(uid, this.settings, this.guessTheWordService);
        });
    }
    clear = async () => {
        const gameModel = this.gameModel;
        if (gameModel) {
            await this.guessTheWordService.restart(gameModel.uid);
        }
        runInAction(() => {
            this.gameModel = undefined;
            this.currentGameInfo = this.gameInfosStorage.defaultGameInfo;
            this.settings = new SettingsModel();
        });
    }
    check() : string {
        if (!this.currentGameInfo.uid) {
            return "Не выбрана игра";
        }

        const settings = this.settings;
        if (settings.attempts <= 1) {
            return "Количество попыток слишком мало";
        }

        if (settings.lettersCount <= 2) {
            return "Количество букв слишком мало";
        }

        return "";
    }
    setLang(lang: LangInfo) {
        this.currentLangInfo = lang;
        this.settings.culture = lang.culture;
    }
}

export class LangInfo {
    constructor(name: string,  culture: string) {
        makeAutoObservable(this);
        this.name = name;
        this.culture = culture;
    }
    name: string = "";
    culture: string = "";
}

export class GameInfo {
    constructor(name: string, uid: string) {
        makeAutoObservable(this);
        this.name = name;
        this.uid = uid;
    }
    name: string = "";
    uid: string = "";
}

export class SettingsModel implements Settings {
    constructor() {
        makeAutoObservable(this);
    }

    SetAttemptsCount(value: number): void {
        this.attempts = value;
    }

    SetLettersCount(value: number) : void {
        this.lettersCount = value;
    }

    SetCulture(value: string) : void {
        this.culture = value;
    }

    culture: string = "ru-RU";
    lettersCount: number = 5;
    attempts: number = 6;
}

export class WordModel implements Word {
    constructor() {
        makeAutoObservable(this);
    }

    stringValue: string = "";
    letters: Letter[] = [];

    setStringValue(val: string) {
        this.stringValue = val;
        const chars = val.split('');
        chars.map((c, index) => {
            const letter = new LetterModel();
            letter.letter = c;
            letter.letterType = LetterType.None;
            letter.position = index;
            this.letters.push(letter);
        })
    }
}

export class LetterModel implements Letter {
    constructor() {
        makeAutoObservable(this);
    }

    letter = "";
    letterType = LetterType.Default;
    position = -1;

    setLetterType(letType: LetterType): void {
        this.letterType = letType;
    }
}