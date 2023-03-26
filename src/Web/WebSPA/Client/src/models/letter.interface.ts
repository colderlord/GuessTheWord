import {makeAutoObservable} from "mobx";

export enum LetterType {
    None,
    Any,
    Fixed,
    Default
}

export class LetterModel {
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

    setLetter(letter: string): void {
        this.letter = letter;
    }

    setPosition(position: number): void {
        this.position = position;
    }
}