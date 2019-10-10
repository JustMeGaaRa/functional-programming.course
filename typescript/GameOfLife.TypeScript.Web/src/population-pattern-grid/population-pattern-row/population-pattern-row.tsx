import React from 'react';
import { WorldRow } from '../../models/WorldRow';
import { OnPatternCellClick } from "../population-pattern-grid";
import PopulationPatternCell from './population-pattern-cell/population-pattern-cell';

type PopulationPatternRowProps = WorldRow & {
    readonly?: boolean;
    onClick?: OnPatternCellClick;
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
                    onClick={props.onClick}
                />
            ))}
        </tr>
    );
}

export default PopulationPatternRow;
