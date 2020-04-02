import React from 'react';
import './population-pattern-grid.css';
import { World } from '../models/World';
import PopulationPatternRow from './population-pattern-row/population-pattern-row';

export type OnPatternCellClick = (
    row: number,
    column: number,
    isAlive: boolean,
    isEmpty: boolean
) => void;

type PopulationPatternProps = World & {
    readonly?: boolean;
    onClick?: OnPatternCellClick;
}

const PopulationPatternGrid: React.FC<PopulationPatternProps> = (props) => {
    return (
        <div className="grid centered">
            <table>
                <tbody>
                    {props.rows.map(row => (
                        <PopulationPatternRow
                            key={`pattern_row_${row.number}`}
                            readonly={props.readonly}
                            number={row.number}
                            columns={row.columns}
                            onClick={props.onClick}
                        />
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default PopulationPatternGrid;
