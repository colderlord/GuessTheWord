import {SettingsModel} from "../storage/Storage";

const webApiUrl = "/api/GuessTheWord";

class GuessTheWordService {
    getAvailableGames = async () => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        headers.append("Accept", "application/json");
        const options = {
            method: "GET",
        }
        const request = new Request(webApiUrl + "/GetAvailableGames", options);
        const response = await fetch(request);
        return response.json();
    }
    setRules = async (gameType: string,  settings: SettingsModel) => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        headers.append("Accept", "application/json");
        var options = {
            method: "POST",
            headers,
            body: JSON.stringify({
                language:settings.culture,
                letters:settings.lettersCount,
                attempts:settings.attempts
            })
        }
        const request = new Request(webApiUrl + "/SetRules?gameType=" + gameType, options);
        const response = await fetch(request);
        return response;
    }
    restart = async (gameType: string) => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        headers.append("Accept", "application/json");
        const options = {
            method: "GET",
        }
        const request = new Request(webApiUrl + "/Restart?gameType=" + gameType, options);
        const response = await fetch(request);
        return response;
    }
    getAvailableLanguages = async () => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        headers.append("Accept", "application/json");
        const options = {
            method: "GET",
        }
        const request = new Request(webApiUrl + "/GetAvailableLanguages", options);
        const response = await fetch(request);
        return response.json();
    }
    sendAnswer = async (gameType: string, word: string) => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        headers.append("Accept", "application/json");
        const options = {
            method: "GET",
        }
        const request = new Request(webApiUrl + "/Send?gameType=" + gameType + "&word=" + word, options);
        const response = await fetch(request);
        return response.json();
    }

}

export default GuessTheWordService;