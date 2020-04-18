import './app.css';
import React from 'react';
import PopulationPatternGrid from './population-pattern-grid/population-pattern-grid';
import ControlPanel from './control-panel/control-panel';
import { PopulationPattern } from './models/PopulationPattern';
import { WorldContainer, World, UserInfo, WorldColumn, WorldRow } from "./models";
import { GameService } from './services/game-service';
import { DropdownProps } from 'semantic-ui-react';

interface AppState {
    worlds: WorldContainer[];
    patterns: Array<PopulationPattern>;
    userId: number;
    isReadonly: boolean;
    isDrawing: boolean;
    isDragging: boolean;
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
        this.handleOnDraggableCaptured = this.handleOnDraggableCaptured.bind(this);
        this.handleOnDraggableReleased = this.handleOnDraggableReleased.bind(this);
        this.handleOnDraggableMoved = this.handleOnDraggableMoved.bind(this);
        this.handleOnCreateClick = this.handleOnCreateClick.bind(this);
        this.handleOnStartClick = this.handleOnStartClick.bind(this);
        this.handleOnStopClick = this.handleOnStopClick.bind(this);
        this.handleOnMouseDown = this.handleOnMouseDown.bind(this);
        this.handleOnMouseUp = this.handleOnMouseUp.bind(this);
        this.handleOnMouseMove = this.handleOnMouseMove.bind(this);
        this.handleOnSelect = this.handleOnSelect.bind(this);

        this.state = {
            worlds: [],
            patterns: [],
            userId: 1,
            isReadonly: false,
            isDrawing: false,
            isDragging: false,
            startX: -1,
            startY: -1,
            endX: -1,
            endY: -1,
            startRow: -1,
            startColumn: -1,
            hoverRow: -1,
            hoverColumn: -1
        };
    }

    render() {
        const { worlds, isReadonly } = this.state;
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
                    onSelect={this.handleOnSelect}
                    onCreateClick={this.handleOnCreateClick}
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
                    {worlds.map(container => (
                        <PopulationPatternGrid
                            readonly={isReadonly}
                            instanceId={container.instanceId}
                            name={container.name}
                            rows={container.world.rows}
                            startX={container.startX}
                            startY={container.startY}
                            onStartClick={this.handleOnStartClick}
                            onStopClick={this.handleOnStopClick}
                            onCellClick={this.handleOnCellMouseClick}
                            onDraggableCaptured={this.handleOnDraggableCaptured}
                            onDraggableReleased={this.handleOnDraggableReleased}
                            onDraggableMoved={this.handleOnDraggableMoved}
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
            this.setState({ worlds: this.mapWorlds(this.state.worlds, data) });
        });
    }
    
    mapWorlds(containers: WorldContainer[], world: World) {
        return containers.find(container => container.instanceId === world.instanceId) == undefined
            ? containers.concat(new WorldContainer("generation: 0", world.instanceId, world, 100, 100))
            : containers.map(container => (
                container.instanceId === world.instanceId
                    ? Object.assign(container, { world })
                    : container
            ));
    }

    generateWorld(startX: number, startY: number, rowCount: number, columnCount: number): WorldContainer {
        return {
            instanceId: '0',
            name: "generation: 0",
            world: {
                instanceId: '0',
                generation: 0,
                height: rowCount,
                width: columnCount,
                rows: this.generateRows(rowCount, columnCount)
            },
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

    handleOnMouseDown(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        this.setState({
            isDrawing: true,
            startX: event.clientX,
            startY: event.clientY
        });
    }

    handleOnMouseUp(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        const { startX, startY, endX, endY } = this.state;

        if (startX >= 0 && startY >= 0 && endX > 0 && endY > 0) {
            const rows = Math.abs(Math.round((endY - startY) / 31));
            const columns = Math.abs(Math.round((endX - startX) / 31));
            const world = this.generateWorld(startX, startY, rows, columns);
            
            this.setState({
                isDrawing: false,
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
        else {
            this.setState({
                isDrawing: false
            });
        }
    }

    handleOnMouseMove(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        if (this.state.isDrawing) {
            this.setState({
                endX: event.clientX,
                endY: event.clientY
            }); 
        }
    }

    handleOnSelect(event: React.SyntheticEvent<HTMLElement, Event>, data: DropdownProps) {
        const { userId } = this.state;
        const patternId = data.value as number;
        this.gameService.end(userId)
            .then(data => {
                const patternsUrl = this.getPatternViewUrl(userId, patternId);
                return this.requestAction<World>(patternsUrl);
            })
            .then(data => this.setState({
                worlds: this.mapWorlds(this.state.worlds, data),
                isReadonly: false
            }))
            .catch(error => { console.log(error); });
    }

    handleOnCreateClick(event: any, selectedPatternId: number) {
        const { userId } = this.state;
        const patternsUrl = this.getPatternUrl(userId);
        const data = new PopulationPattern(0, "newPatternName", 0, 0);

        this.requestAction<PopulationPattern>(patternsUrl, data, "POST")
            .then(data => {
                this.setState({ patterns: this.state.patterns.concat(data) });
                const patternsUrl = this.getPatternViewUrl(userId, selectedPatternId);
                return this.requestAction<World>(patternsUrl);
            })
            .then(data => this.setState({
                worlds: this.mapWorlds(this.state.worlds, data)
            }))
            .catch(error => { console.log(error); });
    }

    handleOnStartClick(event: any, selectedPatternId: string) {
        const { userId } = this.state;
        const instanceId = 0;
        this.gameService.start(userId, instanceId)
            .then(data => this.setState({ isReadonly: true }))
            .catch(error => { console.log(error); });
    }

    handleOnStopClick(event: any, selectedPatternId: string) {
        this.gameService.end(this.state.userId)
            .then(data => this.setState({ isReadonly: false }))
            .catch(error => { console.log(error); });
    }

    handleOnCellMouseClick(row: number, column: number, isAlive: boolean, isEmpty: boolean) {
        const { userId } = this.state;
        const selectedPatternId = 0;
        const patternsUrl = this.getPatternCellUrl(userId, selectedPatternId);
        const data = new WorldColumn(row, column, !isAlive, isEmpty);

        this.requestAction<World>(patternsUrl, data, "PUT")
            .then(data => this.setState({ worlds: this.mapWorlds(this.state.worlds, data) }))
            .catch(error => { console.log(error); });
    }

    handleOnDraggableCaptured(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        console.log("captured");
        this.setState({
            isDragging: true,
            startX: event.clientX,
            startY: event.clientY
        });
    }

    handleOnDraggableReleased(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        console.log("released");
        if (this.state.isDragging) {
            this.setState({
                endX: event.clientX,
                endY: event.clientY
            });
        }
    }

    handleOnDraggableMoved(event: React.MouseEvent<HTMLDivElement, MouseEvent>) {
        console.log("moved");
        const { startX, startY, endX, endY } = this.state;

        if (startX >= 0 && startY >= 0 && endX > 0 && endY > 0) {
            const rows = Math.abs(Math.round((endY - startY) / 31));
            const columns = Math.abs(Math.round((endX - startX) / 31));
            const world = this.generateWorld(startX, startY, rows, columns);
            
            this.setState({
                isDrawing: false,
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
        else {
            this.setState({
                isDrawing: false
            });
        }
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
