import {makeAutoObservable, reaction} from "mobx";
import {IGameInfo} from "../models/gameInfo.model";
import {IReadynessService} from "./readyness.service";

export class GameInfoService {

    private gameInfoServices: IGameInfoService[];
    private readonly gameInfos: IGameInfo[];

    constructor(gameInfoServices: IGameInfoService[]) {
        makeAutoObservable(this);
        this.gameInfoServices = gameInfoServices;
        this.gameInfos = [];

        gameInfoServices.forEach(gi => {
            reaction(
                _ => gi.ready,
                _ => {
                    this.gameInfos.push(gi.getGameInfo());
                }
            );
        })
    }

    GetInfos() : IGameInfo[] {
        return this.gameInfos;
    }
}

export interface IGameInfoService extends IReadynessService{
    getGameInfo() : IGameInfo
}