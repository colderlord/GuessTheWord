import {Letter, LetterType} from "../interfaces/Letter";
import {makeAutoObservable} from "mobx";

export class LetterModel implements Letter {
    constructor(letter: string, letterType: LetterType = LetterType.Default, position: number = -1) {
        makeAutoObservable(this);
        this.letter = letter;
        this.letterType = letterType;
        this.position = position;
    }

    letter: string;
    letterType: LetterType;
    position: number;

    setLetterType(letType: LetterType): void {
        this.letterType = letType;
    }
}