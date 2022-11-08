import {SettingsModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";
import {makeObservable, observable} from "mobx";
import GameModel from "./GameModel";
import {Word} from "../../interfaces/Word";

export default class GuessGameModel extends GameModel {
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        super(uid, settings, guessTheWordService);
        makeObservable(this, {
            wordModel: observable,
        });
    }

    wordModel: Word[] = [];
}