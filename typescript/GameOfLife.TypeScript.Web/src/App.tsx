import React from 'react';
import './app.css';
import WorldGrid from "./world-grid/world-grid";
import { World, PopulationPattern } from './models/PopulationPattern';

interface AppState {
    world: World;
    patterns: Array<PopulationPattern>;
}

class App extends React.Component<{}, AppState> {

    constructor(props: AppState) {
        super(props);

        this.state = {
            world: new World(0, 0, []),
            patterns: []
        };
    }

    render() {
        const { width, height, rows } = this.state.world;
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
                <WorldGrid width={width} height={height} rows={rows} />
            </div>
        );
    }

    componentDidMount() {
        const worldUrl = `https://localhost:44370/api/worlds/${1}/game`;
        const patternsUrl = `https://localhost:44370/api/users/${1}/worlds`;
        const worldPromise = this.requestData<World>(worldUrl);
        const patternsPromise = this.requestData<Array<PopulationPattern>>(patternsUrl);
        Promise.all([worldPromise, patternsPromise])
            .then(data => this.setState({
                world: data[0],
                patterns: data[1]
            }))
            .catch(error => {
                console.log(error);
            });
    }

    handleOnSelect(event: React.ChangeEvent<HTMLSelectElement>) {
        const worldUrl = `https://localhost:44370/api/worlds/${event.target.value}/game`;
        const worldPromise = this.requestData<World>(worldUrl);
        worldPromise
            .then(data => this.setState({
                world: data
            }))
    }

    async requestData<T>(url: string): Promise<T> {
        try {
            const headers = new Headers({
                "Content-Type": "application/json",
            });
            const options = {
                method: "GET",
                headers: headers
            };
            const response = await fetch(url, options);
            const data = await response.json();
            return data;
        } catch (error) {
            return error;
        }
    }
} 

export default App;
