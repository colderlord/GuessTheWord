import {makeAutoObservable, runInAction} from "mobx";
import GuessTheWordService from "../services/GuessTheWordService";
import {GameInfo} from "./Storage";

export class GameInfoStorage {
    defaultGameInfo = new GameInfo("Не выбрано", "");

    constructor(guessTheWordService: GuessTheWordService) {
        makeAutoObservable(this);
        this.guessTheWordService = guessTheWordService;
        this.gameInfos = [this.defaultGameInfo];
    }

    guessTheWordService: GuessTheWordService;

    gameInfos: GameInfo[] = [];
    loadingInfos: boolean = false;

    getGameInfosAsync = async () => {
        if (this.gameInfos.length <= 1) {
            this.loadingInfos = true;
            try {
                const res = await this.guessTheWordService.getAvailableGames();
                runInAction(() => {
                    const gameInfos = res.map((v: any) => new GameInfo(v.name, v.uid));
                    this.setGameInfos(gameInfos);
                    this.loadingInfos = false;
                });
            }
            catch (e) {
                runInAction(() => {
                    this.loadingInfos = false;
                });
            }
        }

        return this.gameInfos;
    }

    setGameInfos(gameInfos: GameInfo[]) {
        this.gameInfos.splice(0);
        this.gameInfos.push(this.defaultGameInfo);
        this.gameInfos.push(...gameInfos);
    }
}