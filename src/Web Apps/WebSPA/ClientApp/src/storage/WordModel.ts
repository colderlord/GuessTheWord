import {Word} from "../interfaces/Word";
import {makeAutoObservable} from "mobx";
import {Letter, LetterType} from "../interfaces/Letter";
import {LetterModel} from "./LetterModel";

export class WordModel implements Word {
    constructor() {
        makeAutoObservable(this);
    }

    stringValue: string = "";
    letters: Letter[] = [];

    setStringValue(val: string, needParse: boolean) {
        this.stringValue = val;
        if (needParse) {
            this.setLetters(WordModel.ParseWord(val));
        }
    }

    setLetters(letters : Letter[]) {
        this.letters = letters;
    }

    static ParseWord(val: string) : Letter[] {
        const chars = val.split('');
        return chars.map((c, index) => {
            return new LetterModel(c, LetterType.None, index);
        })
    }
}