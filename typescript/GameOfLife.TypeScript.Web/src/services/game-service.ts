import { HubConnectionState, HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { World } from "../models/World";

export class GameService {
    private connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl("https://localhost:44370/game")
            .build();
    }
    
    public connect() {
        if (this.connection.state === HubConnectionState.Disconnected) {
            this.connection.start();
        }
    }

    public subscribe(func: (world: World) => void) {
        this.connection.on("UpdateGameWorld", func);
    }
    
    public start(userId: number, patternId: number) {
        if (this.connection.state === HubConnectionState.Connected) {
            this.connection.invoke("StartGameFromPattern", userId, patternId);
        }
    }

    public end(userId: number) {
        if (this.connection.state === HubConnectionState.Connected) {
            this.connection.invoke("EndUserGame", userId);
        }
    }
}

