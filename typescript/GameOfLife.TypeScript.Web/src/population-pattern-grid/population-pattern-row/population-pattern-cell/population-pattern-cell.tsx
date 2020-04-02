import React from 'react';
import './population-pattern-cell.css';
import { OnPatternCellClick } from "../../population-pattern-grid";
import { WorldColumn } from '../../../models/WorldColumn';

type PopulationPatternCellProps = WorldColumn & {
    readonly?: boolean;
    onClick?: OnPatternCellClick;
}

const PopulationPatternCell: React.FC<PopulationPatternCellProps> = (props) => {
    const className = props.isEmpty
        ? "population none"
        : (props.isAlive
            ? "population alive"
            : "population dead");
    const onClick = (event: any) => {
        props.readonly === false
        && props.onClick
        && props.onClick(props.row, props.column, props.isAlive, props.isEmpty);
    };
    
    return (
        <td>
            <div className="cell"onClick={onClick}>
                <div className={className} />
            </div>
        </td>
    );
}

export default PopulationPatternCell;
