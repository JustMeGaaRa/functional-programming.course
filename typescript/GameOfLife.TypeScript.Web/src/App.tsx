import React from 'react';
import './app.css';
import PopulationPatternGrid from "./population-pattern-grid/population-pattern-grid";
import { PopulationPattern } from './models/PopulationPattern';
import { World } from "./models/World";
import { GameService } from "./services/game-service";

interface AppState {
    world: World;
    patterns: Array<PopulationPattern>;
    userId: number;
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
            world: new World(0, 0, 0, []),
            patterns: new Array<PopulationPattern>(0),
            userId: 1,
            newPatternName: "New Pattern",
            newPatternWidth: 1,
            newPatternHeight: 1,
            selectedPatternId: 1
        };
    }

    render() {
        const { generation, width, height, rows } = this.state.world;
        const { patterns, newPatternName, newPatternWidth, newPatternHeight } = this.state;

        return (
            <div className="app layout">
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
        const patternsUrl = `https://localhost:44370/api/users/${this.state.userId}/worlds`;
        this.requestData<Array<PopulationPattern>>(patternsUrl)
            .then(data => this.setState({ patterns: data }))
            .catch(error => { console.log(error); });
        this.gameService.connect();
        this.gameService.subscribe(data => {
            this.setState({ world: data });
        });
    }

    handleOnSelect(event: React.ChangeEvent<HTMLSelectElement>) {
        const patternId = parseInt(event.target.value);
        this.gameService.end(this.state.userId);
        this.setState({ selectedPatternId: patternId })
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
        const { newPatternName, newPatternWidth, newPatternHeight } = this.state;
        const data = new PopulationPattern(0, newPatternName, newPatternWidth, newPatternHeight);
        const patternsUrl = `https://localhost:44370/api/worlds`;
        this.requestAction<PopulationPattern>(patternsUrl, data)
            .then(data => this.setState({ patterns: this.state.patterns.concat(data) }))
            .catch(error => { console.log(error); });
    }

    handleOnStartClick(event: any) {
        const { userId, selectedPatternId } = this.state;
        this.gameService.start(userId, selectedPatternId);
    }

    handleOnStopClick(event: any) {
        this.gameService.end(this.state.userId);
    }

    handleOnPatternCellClick(row: number, column: number, isAlive: boolean) {
        const patternsUrl = `https://localhost:44370/api/users/${this.state.userId}/worlds`;
        this.requestData<World>(patternsUrl);
    }

    async requestData<T>(url: string): Promise<T> {
        const headers = new Headers({
            "Content-Type": "application/json",
        });
        const options = {
            method: "GET",
            headers: headers
        };
        const response = await fetch(url, options);
        return await response.json();
    }

    async requestAction<T>(url: string, body: any): Promise<T> {
        const headers = new Headers({
            "Content-Type": "application/json",
        });
        const options = {
            method: "POST",
            headers: headers,
            body: JSON.stringify(body)
        };
        const response = await fetch(url, options);
        return await response.json();
    }
} 

export default App;
