import React from 'react';
import './app.css';
import PopulationPatternGrid from "./population-pattern-grid/population-pattern-grid";
import { PopulationPattern } from './models/PopulationPattern';
import { World, UserInfo, WorldColumn } from "./models";
import { GameService } from "./services/game-service";

interface AppState {
    selectedWorld: World;
    patterns: Array<PopulationPattern>;
    userId: number;
    isReadonly: boolean;
    newPatternName: string;
    newPatternWidth: number;
    newPatternHeight: number;
    selectedPatternId: number;
}

class App extends React.Component<{}, AppState> {
    private gameService: GameService;

    constructor(props: AppState) {
        super(props);

        this.gameService = new GameService();
        this.handleOnPatternCellClick = this.handleOnPatternCellClick.bind(this);
        this.handleOnCreateClick = this.handleOnCreateClick.bind(this);
        this.handleOnStartClick = this.handleOnStartClick.bind(this);
        this.handleOnStopClick = this.handleOnStopClick.bind(this);
        this.handleOnNameChange = this.handleOnNameChange.bind(this);
        this.handleOnWidthChange = this.handleOnWidthChange.bind(this);
        this.handleOnHeightChange = this.handleOnHeightChange.bind(this);

        this.state = {
            selectedWorld: new World(0, 0, 0, []),
            selectedPatternId: 1,
            patterns: new Array<PopulationPattern>(0),
            userId: 1,
            isReadonly: false,
            newPatternName: "New Pattern",
            newPatternWidth: 10,
            newPatternHeight: 10,
        };
    }

    render() {
        const { generation, width, height, rows } = this.state.selectedWorld;
        const { isReadonly, patterns, newPatternName, newPatternWidth, newPatternHeight } = this.state;

        return (
            <div className="layout">
                <section>
                    <select onChange={this.handleOnSelect.bind(this)}>
                        {patterns.map(pattern => (
                            <option key={pattern.patternId} value={pattern.patternId}>
                                {pattern.name}
                            </option>
                        ))}
                    </select>
                    <button onClick={this.handleOnStartClick}>Start</button>
                    <button onClick={this.handleOnStopClick}>Stop</button>
                </section>
                <section>
                    <input
                        type="text"
                        placeholder="Name"
                        minLength={3}
                        maxLength={30}
                        defaultValue={newPatternName}
                        onChange={this.handleOnNameChange}
                    />
                    <input
                        type="number"
                        placeholder="Width"
                        min={1}
                        max={50}
                        defaultValue={newPatternWidth}
                        onChange={this.handleOnWidthChange}
                    />
                    <input
                        type="number"
                        placeholder="Height"
                        min={1}
                        max={50}
                        defaultValue={newPatternHeight}
                        onChange={this.handleOnHeightChange}
                    />
                    <button onClick={this.handleOnCreateClick}>Create</button>
                </section>
                <p>Generation: {generation}</p>
                <PopulationPatternGrid
                    readonly={isReadonly}
                    generation={generation}
                    width={width}
                    height={height}
                    rows={rows}
                    onClick={this.handleOnPatternCellClick}
                />
            </div>
        );
    }

    componentDidMount() {
        const userInfoUrl = "https://localhost:44370/api/users";

        this.requestAction<UserInfo>(userInfoUrl, {}, "POST")
            .then(data => {
                const patternsUrl = this.getPatternUrl(data.userId);
                this.setState({ userId: data.userId });
                return this.requestAction<Array<PopulationPattern>>(patternsUrl);
            })
            .then(data => this.setState({ patterns: data }))
            .catch(error => { console.log(error); });
        
        this.gameService.connect();
        this.gameService.subscribe(data => {
            this.setState({ selectedWorld: data });
        });
    }

    handleOnSelect(event: React.ChangeEvent<HTMLSelectElement>) {
        const { userId } = this.state;
        const patternId = parseInt(event.target.value);
        this.gameService.end(userId)
            .then(data => {
                this.setState({ selectedPatternId: patternId });
                const patternsUrl = this.getPatternViewUrl(userId, patternId);
                return this.requestAction<World>(patternsUrl);
            })
            .then(data => this.setState({ selectedWorld: data, isReadonly: false }))
            .catch(error => { console.log(error); });
    }

    handleOnNameChange(event: React.ChangeEvent<HTMLInputElement>) {
        this.setState({ newPatternName: event.target.value })
    }

    handleOnWidthChange(event: React.ChangeEvent<HTMLInputElement>) {
        this.setState({ newPatternWidth: parseInt(event.target.value) })
    }

    handleOnHeightChange(event: React.ChangeEvent<HTMLInputElement>) {
        this.setState({ newPatternHeight: parseInt(event.target.value) })
    }

    handleOnCreateClick(event: any) {
        const { userId, selectedPatternId, newPatternName, newPatternWidth, newPatternHeight } = this.state;
        const patternsUrl = this.getPatternUrl(userId);
        const data = new PopulationPattern(0, newPatternName, newPatternWidth, newPatternHeight);

        this.requestAction<PopulationPattern>(patternsUrl, data, "POST")
            .then(data => {
                this.setState({ patterns: this.state.patterns.concat(data) });
                const patternsUrl = this.getPatternViewUrl(userId, selectedPatternId);
                return this.requestAction<World>(patternsUrl);
            })
            .then(data => this.setState({ selectedWorld: data }))
            .catch(error => { console.log(error); });
    }

    handleOnStartClick(event: any) {
        const { userId, selectedPatternId } = this.state;
        this.gameService.start(userId, selectedPatternId)
            .then(data => this.setState({ isReadonly: true }))
            .catch(error => { console.log(error); });
    }

    handleOnStopClick(event: any) {
        this.gameService.end(this.state.userId)
            .then(data => this.setState({ isReadonly: false }))
            .catch(error => { console.log(error); });
    }

    handleOnPatternCellClick(row: number, column: number, isAlive: boolean) {
        const { userId, selectedPatternId } = this.state;
        const patternsUrl = this.getPatternCellUrl(userId, selectedPatternId);
        const data = new WorldColumn(row, column, !isAlive);

        this.requestAction<World>(patternsUrl, data, "PUT")
            .then(data => this.setState({ selectedWorld: data }))
            .catch(error => { console.log(error); });
    }

    getPatternUrl(userId: number) {
        return `https://localhost:44370/api/users/${userId}/patterns`;
    }

    getPatternViewUrl(userId: number, patternId: number) {
        return `https://localhost:44370/api/users/${userId}/patterns/${patternId}/view`;
    }
    
    getPatternCellUrl(userId: number, patternId: number) {
        return `https://localhost:44370/api/users/${userId}/patterns/${patternId}/view/cell`;
    }

    async requestAction<T>(url: string, body: any = undefined, method: string = "GET"): Promise<T> {
        const headers = new Headers({
            "Content-Type": "application/json",
        });
        const options = {
            method: method,
            headers: headers,
            body: JSON.stringify(body)
        };
        const response = await fetch(url, options);
        return await response.json();
    }
} 

export default App;
