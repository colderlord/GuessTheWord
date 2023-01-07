import {makeAutoObservable, reaction, runInAction} from "mobx";
import * as React from "react";
import {ConfigurationService} from "../../../services/configuration.service";
import {StorageService} from "../../../services/storage.service";
import {IGameInfo} from "../../../models/gameInfo.model";
import GuessWordGameIndex from "../components/guessWordGameIndex";
import RouteModel from "../../../models/route";
import {GuessWordGame} from "../components/guessWordGame";
import {IRoutingService} from "../../../services/routing.service";
import {IGameInfoService} from "../../../services/gameInfo.service";
import {IGameService} from "../../../services/game.service";
import {Guessgameitem} from "../models/guessgameitem";

export class GuessWordService implements IRoutingService, IGameInfoService, IGameService {
    private serviceKey = "GuessWordUrl";
    private storage: StorageService;
    private guessWordUrl = '';
    private readonly gameInfo: IGameInfo;
    ready = false;

    constructor(private configurationService: ConfigurationService, private storageService: StorageService) {
        makeAutoObservable(this);
        this.storage = storageService;
        this.gameInfo = {
            uid: "",
            name: "",
            route: ""
        }

        reaction(
            _ => this.configurationService.isReady,
            _ => {
                this.guessWordUrl = this.configurationService.serverSettings.webAggregatorUrl + "/guessword"
                this.storage.store(this.serviceKey, this.guessWordUrl);
                this.ready = true;
            }
        );
    }

    public getGameInfo() : IGameInfo {
        if (this.gameInfo && this.gameInfo.uid) {
            return this.gameInfo;
        }

        if (this.guessWordUrl === '') {
            this.guessWordUrl = this.storage.retrieve(this.serviceKey);
        }

        const url = this.guessWordUrl + '/api/GameInfo';
        fetch(url).then((response) => {
            response.json()
                .then(res => {
                    runInAction(() => {
                        const gameInfo = res as IGameInfo;
                        this.gameInfo.uid = gameInfo.uid;
                        this.gameInfo.name = gameInfo.name;
                        this.gameInfo.route = "/guessGame";
                        return this.gameInfo;
                    });
                });
        });

        return this.gameInfo;
    }

    public routes() : RouteModel[] {
        return [
            {
                path: "/guessGame",
                element: GuessWordGameIndex
            },
            {
                path: "/guessGame/:id",
                element: GuessWordGame
            },
        ]
    }

    list(page: number, size: number) : Promise<Guessgameitem[]> {
        if (this.guessWordUrl === '') {
            this.guessWordUrl = this.storage.retrieve(this.serviceKey);
        }
        const url = this.guessWordUrl + '/api/GuessGame/List?page='+page + "&size=" + size;
        return fetch(url).then((response) => {
            return response.json()
                .then(res => {
                    console.log(res);
                    return res as Guessgameitem[];
                });
        });
    }

    load(id: string) {
        if (this.guessWordUrl === '') {
            this.guessWordUrl = this.storage.retrieve(this.serviceKey);
        }
        const url = this.guessWordUrl + '/api/GuessGame/' + id;
        fetch(url).then((response) => {
            response.json()
                .then(res => {
                    console.log(res);
                    runInAction(() => {
                        
                    });
                });
        });
    }
}