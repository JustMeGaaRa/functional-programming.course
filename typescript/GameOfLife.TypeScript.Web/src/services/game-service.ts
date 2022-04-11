import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { World } from "../models/World";

export class GameService {
    private connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl("https://localhost:44370/game")
            .build();
    }
    
    public connect() {
        return this.connection.start();
    }

    public subscribe(func: (world: World) => void) {
        this.connection.on("UpdateGameWorld", func);
    }
    
    public start(userId: number, patternId: number) {
        return this.connection.invoke("StartGameFromPattern", userId, patternId)
    }

    public end(userId: number) {
        return this.connection.invoke("EndUserGame", userId)
    }
}

