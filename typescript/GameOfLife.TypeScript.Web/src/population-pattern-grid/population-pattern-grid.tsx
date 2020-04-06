import './population-pattern-grid.css';
import React from 'react';
import PopulationPatternRow from './population-pattern-row/population-pattern-row';
import { WorldRow } from '../models';

export type OnPatternCellClick = (
    row: number,
    column: number,
    isAlive: boolean,
    isEmpty: boolean
) => void;

export type OnMouseEvent = (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => void;

type PopulationPatternProps = {
    width: number;
    height: number;
    startX: number;
    startY: number;
    rows: WorldRow[];
    readonly?: boolean;
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

    return (
        <div
            className="grid"
            style={{ left: startX, top: startY }}
        >
            <div className="grid draggable"
                onMouseDown={props.onDraggableCaptured}
                onMouseMove={props.onDraggableMoved}
                onMouseUp={props.onDraggableReleased}
            />
            
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
}

export default PopulationPatternGrid;
