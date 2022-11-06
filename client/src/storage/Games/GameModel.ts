import {Word} from "../../interfaces/Word";
import {LangInfo, LetterModel, SettingsModel, WordModel} from "../Storage";
import {GuessGameUid, TryGuessGameUid, WordsGameUid} from "../Constants";
import GuessTheWordService from "../../services/GuessTheWordService";
import {runInAction} from "mobx";
import {SendWordResponse} from "../../interfaces/SendWordResponse";

export default class GameModel {
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        this.uid = uid;
        this.settings = settings;
        this.guessTheWordService = guessTheWordService;
    }

    guessTheWordService: GuessTheWordService;
    settings: SettingsModel;
    uid: string;
    wordModel: Word[] = [];
    answers: string[] = [];
    AddWord = async(value: string) => {
        if (this.settings.attempts === this.wordModel.length) {
            return;
        }

        const val = value.toLowerCase();
        switch (this.uid) {
            case TryGuessGameUid:
            {
                // Отправить на сервер
                const res = await this.guessTheWordService.sendAnswer(this.uid, val) as SendWordResponse;
                runInAction(() => {
                    this.answers.splice(0);
                    this.answers.push(...res.result);
                });
                break;
            }
            case GuessGameUid:
            {
                const chars = value.toLowerCase().split('');
                const word = new WordModel();
                word.stringValue = val;
                chars.map((v, index) => {
                    const letter = new LetterModel();
                    letter.letter = v;
                    const res = Math.floor((Math.random() * 4) + 1);
                    letter.letterType = res;
                    letter.position = index;
                    word.letters.push(letter);
                });
                this.wordModel.push(word);
                break;
            }
            case WordsGameUid:
            {
                
                break;
            }
            default:
            {
                break;
            }
        }

        // отправить на сервер
    }
    Clear() {
        this.wordModel = [];
    }
    SetUid(uid: string) {
        this.uid = uid;
    }
}