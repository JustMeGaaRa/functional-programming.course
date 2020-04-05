import React from 'react';
import './population-pattern-cell.css';
import { OnPatternCellClick } from "../../population-pattern-grid";
import { WorldColumn } from '../../../models/WorldColumn';

type PopulationPatternCellProps = WorldColumn & {
    readonly?: boolean;
    onMouseClick?: OnPatternCellClick;
    onMouseDown?: OnPatternCellClick;
    onMouseUp?: OnPatternCellClick;
    onMouseOver?: OnPatternCellClick;
}

const PopulationPatternCell: React.FC<PopulationPatternCellProps> = (props) => {
    const cellStyle = props.isEmpty
        ? "cell inactive"
        : "cell active";
    const populationStyle = !props.isEmpty && props.isAlive
        ? "population alive"
        : "population dead";
    const onClick = (event: any) => {
        props.readonly === false
        && props.onMouseClick
        && props.onMouseClick(props.row, props.column, props.isAlive, props.isEmpty);
    };
    const onHover = (event: any) => {
        props.onMouseOver
        && props.onMouseOver(props.row, props.column, props.isAlive, props.isEmpty);
    }
    const onMouseDown = (event: any) => {
        props.onMouseDown
        && props.onMouseDown(props.row, props.column, props.isAlive, props.isEmpty);
    }
    const onMouseUp = (event: any) => {
        props.onMouseUp
        && props.onMouseUp(props.row, props.column, props.isAlive, props.isEmpty);
    }
    
    return (
        <td>
            <div
                className={cellStyle}
                onClick={onClick}
                onMouseDown={onMouseDown}
                onMouseUp={onMouseUp}
                onMouseOver={onHover}
            >
                <div className={populationStyle} />
            </div>
        </td>
    );
}

export default PopulationPatternCell;
