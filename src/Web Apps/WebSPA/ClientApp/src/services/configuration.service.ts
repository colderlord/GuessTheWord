import { IConfiguration } from '../models/configuration.model';
import { StorageService } from './storage.service';
import {makeAutoObservable, runInAction} from 'mobx'

export class ConfigurationService {
    serverSettings: IConfiguration;
    isReady: boolean = false;

    constructor(private storageService: StorageService) { 
        makeAutoObservable(this);
    }
    
    load() {
        const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
        let url = `${baseURI}settings`;
        fetch(url).then((response) => {
            response.json()
                .then(res => {
                    runInAction(() => {
                        console.log('server settings loaded');
                        this.serverSettings = res as IConfiguration;
                        console.log(this.serverSettings);
                        this.storageService.store('identityUrl', this.serverSettings.identityUrl);
                        this.isReady = true;
                    });
                })
            
        });
    }
}
