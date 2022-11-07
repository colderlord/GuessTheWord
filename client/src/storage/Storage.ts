import {makeAutoObservable, runInAction, action} from "mobx"
import {Letter, LetterType} from "../interfaces/Letter";
import {Word} from '../interfaces/Word'
import {Settings} from '../interfaces/Settings'
import GuessTheWordService from "../services/GuessTheWordService";
import GameModel from "./Games/GameModel";

export class Storage {
    defaultGameInfo = new GameInfo("Не выбрано", "");
    defaultLanguage = new LangInfo("Русский", "ru-RU");

    constructor() {
        makeAutoObservable(this);
        this.guessTheWordService = new GuessTheWordService();
        this.settings = new SettingsModel();
        this.currentGameInfo = this.defaultGameInfo;
        this.gameInfos = [this.currentGameInfo];
        this.currentLangInfo = this.defaultLanguage;
        this.languageInfos = [this.currentLangInfo];
    }

    guessTheWordService: GuessTheWordService;

    currentGameInfo: GameInfo;
    gameInfos: GameInfo[] = [];
    currentLangInfo: LangInfo;
    languageInfos: LangInfo[] = [];
    settings: Settings;
    gameModel?: GameModel = undefined;

    getGameInfosAsync = async () => {
        if (this.gameInfos.length <= 1) {
            const res = await this.guessTheWordService.getAvailableGames();
            runInAction(() => {
                const gameInfos = res.map((v: any) => new GameInfo(v.name, v.uid));
                this.setGameInfos(gameInfos);
            });
        }

        return this.gameInfos;
    };
    getLanguagesInfosAsync = async () => {
        if (this.languageInfos.length <= 1) {
            const res = await this.guessTheWordService.getAvailableLanguages();
            runInAction(() => {
                const langInfos = res.map((v: any) => new LangInfo(v.name, v.culture))
                this.setLangInfos(langInfos);
            });
        }

        return this.languageInfos;
    };
    setCurrentGame(gameInfo: GameInfo) {
        this.currentGameInfo = gameInfo;
    }
    setGameInfos(gameInfos: GameInfo[]) {
        this.gameInfos.splice(0);
        this.gameInfos.push(this.defaultGameInfo);
        this.gameInfos.push(...gameInfos);
    }
    async setGameModel(uid: string) {
        await this.guessTheWordService.setRules(uid, this.settings);
        runInAction(() => {
            this.gameModel = new GameModel(uid, this.settings, this.guessTheWordService);
        });
    }
    clear = async () => {
        const gameModel = this.gameModel;
        if (gameModel) {
            await this.guessTheWordService.restart(gameModel.uid);
        }
        runInAction(() => {
            this.gameModel = undefined;
            this.currentGameInfo = this.defaultGameInfo;
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
    setLangInfos(langInfos: LangInfo[]) {
        this.languageInfos.splice(0);
        this.languageInfos.push(...langInfos);
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
}

export class LetterModel implements Letter {
    constructor() {
        makeAutoObservable(this);
    }

    letter = "";
    letterType = LetterType.Default;
    position = -1;
}