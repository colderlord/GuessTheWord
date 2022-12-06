export interface Settings {
    culture : string;
    lettersCount : number;
    attempts : number;
    SetLettersCount(value: number): void;
    SetAttemptsCount(value: number): void;
    SetCulture(value: string): void;
}