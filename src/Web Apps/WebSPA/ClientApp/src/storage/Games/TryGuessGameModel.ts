import {action, makeObservable, observable, runInAction} from "mobx";

import {Word} from "../../interfaces/Word";
import {SettingsModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";
import {SendWordResponse} from "../../interfaces/SendWordResponse";
import {LetterType} from "../../interfaces/Letter";
import GameModel from "./GameModel";
import {WordModel} from "../WordModel";

export default class TryGuessGameModel extends GameModel{
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        super(uid, settings, guessTheWordService);
        makeObservable(this, {
            wordModel: observable,
            answers: observable,
            AddWordModel: action,
            AddWord: action,
            SetAnswers: action,
            Clear: action
        });
    }

    wordModel: Word[] = [];
    answers: string[] = [];

    AddWordModel = async (wordModel: WordModel) => {
        let val: string = "";
        wordModel.letters.map(l => {
            const letter = l.letter;
            switch (l.letterType) {
                case LetterType.None:
                {
                    val += letter;
                    break;
                }
                case LetterType.Any:
                {
                    val += "$"+letter;
                    break;
                }
                case LetterType.Fixed:
                {
                    val += "!"+letter;
                    break;
                }
                case LetterType.Default:
                {
                    val += letter;
                    break;
                }
            }
        })

        this.setError("");
        try {
            // Отправить на сервер
            const res = await this.guessTheWordService.sendAnswer(this.uid, val) as SendWordResponse;
            runInAction(() => {
                if (res.success === true) {
                    this.answers.splice(0);
                    this.answers.push(...res.result);
                } else {
                    this.setError(res.reason);
                }
            });
        }
        catch (e) {
            runInAction(() => {
                this.setError(e as string);
            });
        }
    }

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
                if (res.success === true) {
                    this.SetAnswers(res.result);
                } else {
                    this.setError(res.reason);
                }
            });
        }
        catch (e) {
            runInAction(() => {
                this.setError(e as string);
            });
        }
    }
    SetAnswers(answers: string[]) {
        this.answers.splice(0);
        this.answers.push(...answers);
    }
    Clear() {
        this.wordModel = [];
    }
}