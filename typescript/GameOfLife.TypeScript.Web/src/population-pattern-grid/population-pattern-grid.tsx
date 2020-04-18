import './population-pattern-grid.css';
import React from 'react';
import PopulationPatternRow from './population-pattern-row/population-pattern-row';
import { Popup, Menu } from 'semantic-ui-react';
import { WorldRow } from '../models';

export type OnPatternCellClick = (
    row: number,
    column: number,
    isAlive: boolean,
    isEmpty: boolean
) => void;

export type OnMouseEvent = (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => void;

type PopulationPatternProps = {
    name: string;
    instanceId: string;
    startX: number;
    startY: number;
    rows: WorldRow[];
    readonly?: boolean;
    onStartClick: (event: any, instanceId: string) => void;
    onStopClick: (event: any, instanceId: string) => void;
    onCellClick?: OnPatternCellClick;
    onCellMouseDown?: OnPatternCellClick;
    onCellMouseUp?: OnPatternCellClick;
    onCellHover?: OnPatternCellClick;
    onDraggableCaptured?: OnMouseEvent;
    onDraggableMoved?: OnMouseEvent;
    onDraggableReleased?: OnMouseEvent;
}

const PopulationPatternGrid: React.FC<PopulationPatternProps> = (props) => {
    const { startX, startY } = props;
    const onStartClick = (event: any, data: any) => { props.onStartClick(event, props.instanceId) };
    const onStopClick = (event: any, data: any) => { props.onStartClick(event, props.instanceId) };
    const style = { left: startX, top: startY }
    const grid = (
        <div className="grid" style={style}>
            <table>
                <tbody>
                    {props.rows.map(row => (
                        <PopulationPatternRow
                            key={`pattern_row_${row.number}`}
                            readonly={props.readonly}
                            number={row.number}
                            columns={row.columns}
                            onClick={props.onCellClick}
                            onMouseDown={props.onCellMouseDown}
                            onMouseUp={props.onCellMouseUp}
                            onHover={props.onCellHover}
                        />
                    ))}
                </tbody>
            </table>
        </div>
    );

    return (
        <Popup
            trigger={grid}
            flowing
            hoverable
            basic
            size="mini"
            position="top right"
        >
            <Menu secondary size="mini">
                <Menu.Item name="play" icon="play" onClick={onStartClick} />
                <Menu.Item name="stop" icon="stop" onClick={onStopClick} />
                <Menu.Item name="delete" icon="delete" onClick={() => {}} />
                <Menu.Item name="label" content={props.name} />
            </Menu>
        </Popup>
    );
}

export default PopulationPatternGrid;
