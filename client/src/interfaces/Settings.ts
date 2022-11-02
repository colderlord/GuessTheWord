export interface Settings {
    Culture : string;
    LettersCount : number;
    Attempts : number;
    SetLettersCount(value: number): void;
    SetAttemptsCount(value: number): void;
    SetCulture(value: string): void;
}