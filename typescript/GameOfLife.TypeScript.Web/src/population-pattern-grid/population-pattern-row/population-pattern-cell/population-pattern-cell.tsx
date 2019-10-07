import React from 'react';
import './population-pattern-cell.css';
import { OnPatternCellClick } from "../../population-pattern-grid";
import { WorldColumn } from '../../../models/WorldColumn';

type PopulationPatternCellProps = WorldColumn & {
    onClick?: OnPatternCellClick;
}

const PopulationPatternCell: React.FC<PopulationPatternCellProps> = (props) => {
    const className = props.isAlive
        ? "population alive"
        : "population dead";
    const onClick = (event: any) => {
        props.onClick && props.onClick(props.row, props.column, props.isAlive);
    };
    
    return (
        <td>
            <div
                className={className}
                onClick={onClick}
            />
        </td>
    );
}

export default PopulationPatternCell;
