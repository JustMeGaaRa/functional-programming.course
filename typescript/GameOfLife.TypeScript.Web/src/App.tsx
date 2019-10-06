import React from 'react';
import './app.css';
import WorldGrid from "./world-grid/world-grid";
import { PopulationPattern } from './models/PopulationPattern';
import { World } from "./models/World";
import { GameService } from "./services/game-service";

interface AppState {
    world: World;
    patterns: Array<PopulationPattern>;
}

class App extends React.Component<{}, AppState> {
    private gameService: GameService;

    constructor(props: AppState) {
        super(props);

        this.gameService = new GameService();

        this.state = {
            world: new World(0, 0, 0, []),
            patterns: new Array<PopulationPattern>(0)
        };
    }

    render() {
        const { generation, width, height, rows } = this.state.world;
        const { patterns } = this.state;

        return (
            <div className="app layout">
                <select onChange={this.handleOnSelect.bind(this)}>
                    {patterns.map(pattern => (
                        <option key={pattern.patternId} value={pattern.patternId}>
                            {pattern.name}
                        </option>
                    ))}
                </select>
                <p>Generation: {generation}</p>
                <WorldGrid generation={generation} width={width} height={height} rows={rows} />
            </div>
        );
    }

    componentDidMount() {
        const userId = 1;
        const patternsUrl = `https://localhost:44370/api/users/${userId}/worlds`;
        const patternsPromise = this.requestData<Array<PopulationPattern>>(patternsUrl);
        patternsPromise
            .then(data => this.setState({ patterns: data }))
            .catch(error => { console.log(error); });
        this.gameService.connect();
        this.gameService.subscribe(data => {
            this.setState({ world: data });
        });
    }

    handleOnSelect(event: React.ChangeEvent<HTMLSelectElement>) {
        const userId = 1;
        const patternId = event.target.value;
        this.gameService.end(userId);
        this.gameService.start(userId, parseInt(patternId));
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
} 

export default App;
