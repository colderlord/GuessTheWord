import { LetterModel, LetterType} from "./letter.interface";
import {makeAutoObservable} from "mobx";

export class WordModel {
    constructor() {
        makeAutoObservable(this);
    }

    stringValue: string = "";
    letters: LetterModel[] = [];

    setStringValue(val: string, needParse: boolean) {
        this.stringValue = val;
        if (needParse) {
            this.setLetters(WordModel.ParseWord(val));
        }
    }

    setLetters(letters : LetterModel[]) {
        this.letters = letters;
    }

    static ParseWord(val: string) : LetterModel[] {
        const chars = val.split('');
        return chars.map((c, index) => {
            return new LetterModel(c, LetterType.None, index);
        })
    }
}