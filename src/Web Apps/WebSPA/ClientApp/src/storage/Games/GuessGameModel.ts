import {action, makeObservable, observable, runInAction} from "mobx";

import {SettingsModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";
import GameModel from "./GameModel";
import {Word} from "../../interfaces/Word";
import {SendWordResponse} from "../../interfaces/SendWordResponse";
import {WordModel} from "../WordModel";
import {LetterModel} from "../LetterModel";

export default class GuessGameModel extends GameModel {
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        super(uid, settings, guessTheWordService);
        makeObservable(this, {
            wordModel: observable,
            AddWord: action
        });
    }

    wordModel: Word[] = [];
    AddWord = async(value: string) => {
        if (this.settings.attempts === this.wordModel.length) {
            this.setError("Количество попыток превышает количество попыток в настройках");
            return;
        }

        this.setError("");
        const val = value.toLowerCase();
        try {
            // Отправить на сервер
            const res = await this.guessTheWordService.sendAnswer(this.uid, val) as SendWordResponse;
            runInAction(() => {
                if (res.success === false) {
                    this.setError(res.reason);
                } else {
                    let word = "";
                    const letters = res.result.map((r : any) => {
                        const letterChar = r.value;
                        word+= letterChar;
                        return new LetterModel(letterChar, r.option, r.position);
                    });
                    const wordModel = new WordModel();
                    wordModel.setStringValue(word, false);
                    wordModel.setLetters(letters);
                    this.wordModel.push(wordModel);
                }
            });
        }
        catch (e) {
            runInAction(() => {
                this.setError(e as string);
            });
        }
    }
}