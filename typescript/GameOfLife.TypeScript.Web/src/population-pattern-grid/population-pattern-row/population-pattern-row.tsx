import React from 'react';
import { WorldRow } from '../../models/WorldRow';
import { OnPatternCellClick } from "../population-pattern-grid";
import PopulationPatternCell from './population-pattern-cell/population-pattern-cell';
import { WorldColumn } from '../../models';

type PopulationPatternRowProps = {
    number: number;
    columns: WorldColumn[];
    readonly?: boolean;
    onClick?: OnPatternCellClick;
    onMouseDown?: OnPatternCellClick;
    onMouseUp?: OnPatternCellClick;
    onHover?: OnPatternCellClick;
}

const PopulationPatternRow: React.FC<PopulationPatternRowProps> = (props) => {
    return (
        <tr>
            {props.columns.map(cell => (
                <PopulationPatternCell
                    key={`pattern_cell_${cell.row}_${cell.column}`}
                    readonly={props.readonly}
                    row={cell.row}
                    column={cell.column}
                    isAlive={cell.isAlive}
                    isEmpty={cell.isEmpty}
                    onMouseClick={props.onClick}
                    onMouseDown={props.onMouseDown}
                    onMouseUp={props.onMouseUp}
                    onMouseOver={props.onHover}
                />
            ))}
        </tr>
    );
}

export default PopulationPatternRow;
