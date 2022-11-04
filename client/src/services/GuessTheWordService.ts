const serverName = "https://localhost:62275"
const webApiUrl = "/api/GuessTheWord";

// "https://localhost:62275/api/GuessTheWord/GetAvailableGames"

class GuessTheWordService {

    getAvailableGames = async () => {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        //Accept': 'application/json'
        headers.append("Accept", "application/json");
        const options = {
            method: "GET",
            mode: "cors" as RequestMode,
            //mode: "no-cors" as RequestMode,
    
        }
        const request = new Request(webApiUrl + "/GetAvailableGames", options);
        const response = await fetch(request);
        return response.json();
    }
}

export default GuessTheWordService;