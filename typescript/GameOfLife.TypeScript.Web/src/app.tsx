import './app.css';
import React from 'react';
import PopulationPatternGrid from './population-pattern-grid/population-pattern-grid';
import ControlPanel from './control-panel/control-panel';
import { PopulationPattern } from './models/PopulationPattern';
import { World, UserInfo, WorldColumn, WorldRow } from "./models";
import { GameService } from './services/game-service';

interface AppState {
    worlds: World[];
    selectedWorld: World;
    patterns: Array<PopulationPattern>;
    userId: number;
    isReadonly: boolean;
    newPatternName: string;
    newPatternWidth: number;
    newPatternHeight: number;
    selectedPatternId: number;
    isMouseDown: boolean;
    startX: number;
    startY: number;
    endX: number;
    endY: number;
    startRow: number;
    startColumn: number;
    hoverRow: number;
    hoverColumn: number;
}

class App extends React.Component<{}, AppState> {
    private gameService: GameService;

    constructor(props: AppState) {
        super(props);

        this.gameService = new GameService();
        this.handleOnCellMouseClick = this.handleOnCellMouseClick.bind(this);
        this.handleOnCellMouseDown = this.handleOnCellMouseDown.bind(this);
        this.handleOnCellMouseUp = this.handleOnCellMouseUp.bind(this);
        this.handleOnCellMouseOver = this.handleOnCellMouseOver.bind(this);
        this.handleOnCreateClick = this.handleOnCreateClick.bind(this);
        this.handleOnStartClick = this.handleOnStartClick.bind(this);
        this.handleOnStopClick = this.handleOnStopClick.bind(this);
        this.handleOnNameChange = this.handleOnNameChange.bind(this);
        this.handleOnWidthChange = this.handleOnWidthChange.bind(this);
        this.handleOnHeightChange = this.handleOnHeightChange.bind(this);
        this.handleOnMouseDown = this.handleOnMouseDown.bind(this);
        this.handleOnMouseUp = this.handleOnMouseUp.bind(this);
        this.handleOnMouseMove = this.handleOnMouseMove.bind(this);

        this.state = {
            worlds: [],
            selectedWorld: new World(0, 0, 0, [], 0, 0),
            selectedPatternId: 1,
            patterns: new Array<PopulationPattern>(0),
            userId: 1,
            isReadonly: false,
            newPatternName: "New Pattern",
            newPatternWidth: 10,
            newPatternHeight: 10,
            isMouseDown: false,
            startX: 0,
            startY: 0,
            endX: 0,
            endY: 0,
            startRow: -1,
            startColumn: -1,
            hoverRow: -1,
            hoverColumn: -1
        };
    }

    render() {
        const { worlds, isReadonly, patterns, newPatternName, newPatternWidth, newPatternHeight } = this.state;
        const { startX, startY, endX, endY } = this.state;
        const selectionWidth = endX - startX;
        const selectionHeight = endY - startY;
        const  rectStyle = {
            left: startX,
            top: startY,
            width: selectionWidth,
            height: selectionHeight
        };

        return (
            <div className="layout">
                <ControlPanel
                    patterns={this.state.patterns}
                    handleOnSelect={this.handleOnSelect}
                    handleOnStartClick={this.handleOnStartClick}
                    handleOnStopClick={this.handleOnStopClick}
                    handleOnCreateClick={this.handleOnCreateClick}
                    handleOnNameChange={this.handleOnNameChange}
                    handleOnHeightChange={this.handleOnHeightChange}
                    handleOnWidthChange={this.handleOnWidthChange}
                />
                <div
                    style={{ position: "relative", flexGrow: 1 }}
                    onMouseDown={this.handleOnMouseDown}
                    onMouseUp={this.handleOnMouseUp}
                    onMouseMove={this.handleOnMouseMove}
                >
                    <div
                        className="selection-rect"
                        style={rectStyle}
                    />  
                    {worlds.map(world => (
                        <PopulationPatternGrid
                            readonly={isReadonly}
                            {...world}
                            onClick={this.handleOnCellMouseClick}
                            onMouseDown={this.handleOnCellMouseDown}
                            onMouseUp={this.handleOnCellMouseUp}
                            onHover={this.handleOnCellMouseOver}
                        />
                    ))}
                </div>
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

    generateWorld(startX: number, startY: number, rowCount: number, columnCount: number): World {
        return {
            generation: 0,
            height: rowCount,
            width: columnCount,
            rows: this.generateRows(rowCount, columnCount),
            startX: startX,
            startY: startY
        }
    }

    generateRows(rowCount: number, columnCount: number): Array<WorldRow> {
        return Array.from(new Array(rowCount)).map<WorldRow>((value, index) => {
            return {
                columns: this.generateColumns(index, columnCount),
                number: index
            };
        });
    }

    generateColumns(row: number, count: number): Array<WorldColumn> {
        return Array.from(new Array(count)).map<WorldColumn>((value, index) => {
            return {
                column: index,
                isAlive: false,
                isEmpty: true,
                row: row
            };
        });
    }

    roundCoordinates(coordinate: number) {
        return coordinate - coordinate % 31;
    }

    handleOnMouseDown(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        this.setState({
            isMouseDown: true,
            startX: event.clientX,
            startY: event.clientY
        });
    }

    handleOnMouseUp(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        const { startX, startY, endX, endY } = this.state;
        const rows = Math.abs(Math.round((endY - startY) / 31));
        const columns = Math.abs(Math.round((endX - startX) / 31));
        const world = this.generateWorld(startX, startY, rows, columns);

        this.setState({
            isMouseDown: false,
            startX: -1,
            startY: -1,
            endX: -1,
            endY: -1,
            startRow: -1,
            startColumn: -1,
            hoverRow: -1,
            hoverColumn: -1,
            worlds: this.state.worlds.concat(world)
        });
    }

    handleOnMouseMove(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        if (this.state.isMouseDown) {
            this.setState({
                endX: this.roundCoordinates(event.clientX),
                endY: this.roundCoordinates(event.clientY)
            }); 
        }
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

    handleOnCellMouseClick(row: number, column: number, isAlive: boolean, isEmpty: boolean) {
        const { userId, selectedPatternId } = this.state;
        const patternsUrl = this.getPatternCellUrl(userId, selectedPatternId);
        const data = new WorldColumn(row, column, !isAlive, isEmpty);

        this.requestAction<World>(patternsUrl, data, "PUT")
            .then(data => this.setState({ selectedWorld: data }))
            .catch(error => { console.log(error); });
    }

    handleOnCellMouseDown(row: number, column: number, isAlive: boolean, isEmpty: boolean) {
        
    }

    handleOnCellMouseUp(row: number, column: number, isAlive: boolean, isEmpty: boolean) {
        
    }

    handleOnCellMouseOver(row: number, column: number, isAlive: boolean, isEmpty: boolean) {
        
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
