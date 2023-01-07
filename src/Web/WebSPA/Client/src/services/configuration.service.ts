import { IConfiguration } from '../models/configuration.model';
import { StorageService } from './storage.service';
import {makeAutoObservable, runInAction} from 'mobx'

export class ConfigurationService {
    serverSettings: IConfiguration;
    isReady: boolean = false;

    constructor(private storageService: StorageService) { 
        makeAutoObservable(this);
        this.serverSettings = {
            identityUrl: "",
            webAggregatorUrl: ""
        };
    }
    
    load() {
        const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
        let url = `${baseURI}settings`;
        fetch(url).then((response) => {
            response.json()
                .then(res => {
                    runInAction(() => {
                        this.serverSettings = res as IConfiguration;
                        this.storageService.store('identityUrl', this.serverSettings.identityUrl);
                        this.storageService.store('webAggregatorUrl', this.serverSettings.webAggregatorUrl);
                        this.isReady = true;
                    });
                })
            
        });
    }
}
