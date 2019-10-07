import React from 'react';
import './population-pattern-grid.css';
import { World } from '../models/World';
import PopulationPatternRow from './population-pattern-row/population-pattern-row';

export type OnPatternCellClick = (row: number, column: number, isAlive: boolean) => void;

type PopulationPatternProps = World & {
    onClick?: OnPatternCellClick;
}

const PopulationPatternGrid: React.FC<PopulationPatternProps> = (pattern) => {
    return (
        <div className="grid centered">
            <table>
                <tbody>
                    {pattern.rows.map(row => (
                        <PopulationPatternRow
                            key={`pattern_row_${row.number}`}
                            number={row.number}
                            columns={row.columns}
                            onClick={pattern.onClick}
                        />
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default PopulationPatternGrid;
