import {action, makeObservable, observable} from "mobx";

import {SettingsModel} from "../Storage";
import GuessTheWordService from "../../services/GuessTheWordService";

export default class GameModel {
    constructor(uid: string, settings: SettingsModel, guessTheWordService: GuessTheWordService) {
        makeObservable(this, {
            uid: observable,
            settings: observable,
            guessTheWordService: observable,
            error: observable,
            setError: action
        });
        this.uid = uid;
        this.settings = settings;
        this.guessTheWordService = guessTheWordService;
        this.error = "";
    }

    guessTheWordService: GuessTheWordService;

    uid: string;
    settings: SettingsModel;
    error: string;

    setError(err: string) {
        this.error = err;
    }
}