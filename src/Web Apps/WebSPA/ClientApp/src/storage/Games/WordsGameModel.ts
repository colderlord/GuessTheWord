import {action, makeObservable, observable, runInAction} from "mobx";

import {SettingsModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";
import GameModel from "./GameModel";
import {SendWordResponse} from "../../interfaces/SendWordResponse";
import {Word} from "../../interfaces/Word";
import {WordModel} from "../WordModel";

export default class WordsGameModel extends GameModel {
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        super(uid,  settings, guessTheWordService);
        makeObservable(this, {
            words: observable,
            AddWord: action
        });

        this.words = [];
    }

    words: string[];
    wordModel: Word[] = [];
    AddWord = async(value: string) => {
        const val = value.toLowerCase();
        if (this.words.includes(val)) {
            this.setError("Такое слово уже существует");
            return;
        }

        this.setError("");
        try {
            // Отправить на сервер
            const res = await this.guessTheWordService.sendAnswer(this.uid, val) as SendWordResponse;
            runInAction(() => {
                if (res.success === false) {
                    this.setError(res.reason);
                } else {
                    const wordModel = new WordModel();
                    wordModel.setStringValue(value, true);
                    this.wordModel.push(wordModel);
                    const result = res.result;
                    const resultWordModel = new WordModel();
                    resultWordModel.setStringValue(result, true);
                    this.wordModel.push(resultWordModel);
                    this.words.push(value);
                    this.words.push(result);
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