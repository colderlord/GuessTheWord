import {SettingsModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";
import {action, makeObservable, observable} from "mobx";

export default class GameModel {
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        makeObservable(this, {
            uid: observable,
            settings: observable,
            guessTheWordService: observable
        });
        this.uid = uid;
        this.settings = settings;
        this.guessTheWordService = guessTheWordService;
    }

    guessTheWordService: GuessTheWordService;

    uid: string;
    settings: SettingsModel;
}