import {Word} from "../../interfaces/Word";
import {SettingsModel, WordModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";
import {action, makeObservable, observable, runInAction} from "mobx";
import {SendWordResponse} from "../../interfaces/SendWordResponse";
import {LetterType} from "../../interfaces/Letter";
import GameModel from "./GameModel";

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

        try {
            // Отправить на сервер
            const res = await this.guessTheWordService.sendAnswer(this.uid, val) as SendWordResponse;
            runInAction(() => {
                this.answers.splice(0);
                this.answers.push(...res.result);
            });
        }
        catch (e) {
        }
    }

    AddWord = async(value: string) => {
        if (this.settings.attempts === this.wordModel.length) {
            return;
        }

        const val = value.toLowerCase();
        try {
            // Отправить на сервер
            const res = await this.guessTheWordService.sendAnswer(this.uid, val) as SendWordResponse;
            runInAction(() => {
                this.SetAnswers(res.result);
            });
        }
        catch (e) {
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