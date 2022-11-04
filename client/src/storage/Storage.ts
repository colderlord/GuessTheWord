import {makeAutoObservable, runInAction} from "mobx"
import {Letter, LetterType} from "../interfaces/Letter";
import {Word} from '../interfaces/Word'
import {Settings} from '../interfaces/Settings'
import GuessTheWordService from "../services/GuessTheWordService";

export class Storage {
    defaultGameInfo = {
        uid: "",
        name: "Не выбрано"
    };
    defaultLanguage = {
        culture: "ru-RU",
        name: "Русский"
    };

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
            //const res = await this.guessTheWordService.getAvailableGames();
            const res =   [{
                    name: "Ты пытаешься отгадать слово",
                    uid: "793370cf-7ea5-4d45-af27-65def7293e40"
                },
                {
                    name: "Я (компьютер) пытаюсь отгадать слово",
                    uid: "b32bb668-5448-4b1b-89c9-31138bc6eb63"
                },
                {
                    name: "Игра в слова",
                    uid: "b5451229-e47d-4fca-ad1b-c787c34d6279"
                }];
            runInAction(() => {
                this.setGameInfos(res);
            });
        }

        return this.gameInfos;
    };
    getLanguagesInfosAsync = async () => {
        if (this.languageInfos.length <= 1) {
            //const res = await this.guessTheWordService.getAvailableLanguages();
            const res = [{
                culture: "ru-RU",
                name: "Русский"
            },
                {
                    culture: "en-US",
                    name: "English"
                }];
            runInAction(() => {
                this.setLangInfos(res);
            });
        }

        return this.gameInfos;
    };
    setCurrentGame(gameInfo: GameInfo) {
        this.currentGameInfo = gameInfo;
    }
    setGameInfos(gameInfos: GameInfo[]) {
        this.gameInfos.push(...gameInfos);
    }
    setGameModel(uid: string) {
        this.gameModel = new GameModel(uid, this.settings);
    }
    clear() {
        this.gameModel = undefined;
        this.currentGameInfo = this.defaultGameInfo;
        this.settings = new SettingsModel();
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

export class GameModel {
    constructor(uid: string, settings: SettingsModel) {
        makeAutoObservable(this);
        this.uid = uid;
        this.settings = settings;
    }

    settings: SettingsModel;
    uid: string;
    wordModel: Word[] = [];
    AddWord(value: string) {
        if (this.settings.attempts == this.wordModel.length) {
            return;
        }

        const val = value.toLowerCase();
        const chars = value.toLowerCase().split('');
        const word = new WordModel();
        word.stringValue = val;
        chars.map((v, index) => {
            const letter = new LetterModel();
            letter.letter = v;
            var res = Math.floor((Math.random() * 4) + 1);
            letter.letterType = res;
            letter.position = index;
            word.letters.push(letter);
        });
        this.wordModel.push(word);
        // отправить на сервер
    }
    Clear() {
        this.wordModel = [];
    }
    SetUid(uid: string) {
        this.uid = uid;
    }
}

export class LangInfo {
    name: string = "";
    culture: string = "";
}

export class GameInfo {
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