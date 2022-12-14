import {makeAutoObservable, runInAction} from "mobx"
import {Settings} from '../interfaces/Settings'
import GuessTheWordService from "../services/GuessTheWordService";
import TryGuessGameModel from "./Games/TryGuessGameModel";
import {GameInfoStorage} from "./GameInfoStorage";
import {LangInfoStorage} from "./LangInfoStorage";
import {GuessGameUid, TryGuessGameUid, WordsGameUid} from "./Constants";
import GuessGameModel from "./Games/GuessGameModel";
import GameModel from "./Games/GameModel";
import WordsGameModel from "./Games/WordsGameModel";

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
    gameModel?: GameModel = undefined;

    setCurrentGame(gameInfo: GameInfo) {
        this.currentGameInfo = gameInfo;
    }
    async setGameModel(uid: string) {
        await this.guessTheWordService.setRules(uid, this.settings);
        runInAction(() => {
            switch (uid) {
                case TryGuessGameUid: {
                    this.gameModel = new TryGuessGameModel(uid, this.settings, this.guessTheWordService);
                    break;
                }
                case GuessGameUid: {
                    this.gameModel = new GuessGameModel(uid, this.settings, this.guessTheWordService);
                    break;
                }
                case WordsGameUid: {
                    this.gameModel = new WordsGameModel(uid, this.settings, this.guessTheWordService);
                    break;
                }
            }
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
            return "???? ?????????????? ????????";
        }

        const settings = this.settings;
        if (settings.attempts <= 1) {
            return "???????????????????? ?????????????? ?????????????? ????????";
        }

        if (settings.lettersCount <= 2) {
            return "???????????????????? ???????? ?????????????? ????????";
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