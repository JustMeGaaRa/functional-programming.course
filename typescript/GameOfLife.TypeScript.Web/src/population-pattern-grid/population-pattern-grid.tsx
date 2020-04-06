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

type PopulationPatternProps = {
    width: number;
    height: number;
    startX: number;
    startY: number;
    rows: WorldRow[];
    readonly?: boolean;
    onClick?: OnPatternCellClick;
    onMouseDown?: OnPatternCellClick;
    onMouseUp?: OnPatternCellClick;
    onHover?: OnPatternCellClick;
}

const PopulationPatternGrid: React.FC<PopulationPatternProps> = (props) => {
    const { startX, startY } = props;

    return (
        <div
            className="grid"
            style={{ left: startX, top: startY }}
        >
            <div className="grid draggable" />
            
            <table>
                <tbody>
                    {props.rows.map(row => (
                        <PopulationPatternRow
                        key={`pattern_row_${row.number}`}
                        readonly={props.readonly}
                        number={row.number}
                        columns={row.columns}
                        onClick={props.onClick}
                        onMouseDown={props.onMouseDown}
                        onMouseUp={props.onMouseUp}
                        onHover={props.onHover}
                        />
                        ))}
                </tbody>
            </table>
        </div>
    );
}

export default PopulationPatternGrid;
