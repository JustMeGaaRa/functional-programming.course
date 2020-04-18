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
        return this.connection.state === HubConnectionState.Disconnected
            ? this.connection.start()
            : Promise.resolve();
    }

    public subscribe(func: (world: World) => void) {
        this.connection.on("UpdateGameWorld", func);
    }

    public create(userId: number, patternId: number) {
        return this.connection.state === HubConnectionState.Connected
            ? this.connection.invoke("CreateFromPattern", userId, patternId)
            : Promise.resolve();
    }
    
    public start(userId: number, instanceId: string) {
        return this.connection.state === HubConnectionState.Connected
            ? this.connection.invoke("StartUserGame", userId, instanceId)
            : Promise.resolve();
    }

    public end(userId: number, instanceId: string) {
        return this.connection.state === HubConnectionState.Connected
            ? this.connection.invoke("EndUserGame", userId, instanceId)
            : Promise.resolve();
    }
}

