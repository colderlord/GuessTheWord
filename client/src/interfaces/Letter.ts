export interface Letter {
    letter: string,
    letterType: LetterType,
    position: number,
    setLetterType(letType: LetterType) : void
}

export enum LetterType {
    None,
    Any,
    Fixed,
    Default
}