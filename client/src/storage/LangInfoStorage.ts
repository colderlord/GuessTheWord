import {makeAutoObservable, runInAction} from "mobx";
import GuessTheWordService from "../services/GuessTheWordService";
import {LangInfo} from "./Storage";

export class LangInfoStorage {
    defaultLanguage = new LangInfo("Русский", "ru-RU");

    constructor(guessTheWordService: GuessTheWordService) {
        makeAutoObservable(this);
        this.guessTheWordService = guessTheWordService;
        this.currentLangInfo = this.defaultLanguage;
        this.languageInfos = [this.currentLangInfo];
    }

    guessTheWordService: GuessTheWordService;

    currentLangInfo: LangInfo;
    languageInfos: LangInfo[] = [];
    loadingInfos: boolean = false;

    getLanguagesInfosAsync = async () => {
        if (this.languageInfos.length <= 1) {
            this.loadingInfos = true;
            try{
                const res = await this.guessTheWordService.getAvailableLanguages();
                runInAction(() => {
                    const langInfos = res.map((v: any) => new LangInfo(v.name, v.culture))
                    this.setLangInfos(langInfos);
                    this.loadingInfos = false;
                });
            }
            catch (e) {
                runInAction(() => {
                    this.loadingInfos = false;
                });
            }
        }

        return this.languageInfos;
    }

    setLangInfos(langInfos: LangInfo[]) {
        this.languageInfos.splice(0);
        this.languageInfos.push(...langInfos);
    }

}