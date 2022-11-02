export interface LetterModel {
    letter: string,
    letterType: LetterType,
    position: number
}

export enum LetterType {
    None,
    Any,
    Fixed,
    Default
}