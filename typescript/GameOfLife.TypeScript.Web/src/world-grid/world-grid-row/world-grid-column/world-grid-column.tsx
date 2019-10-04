import React from 'react';
import './world-grid-column.css';
import { WorldColumn } from '../../../models/PopulationPattern';

const WorldGridColumn: React.FC<WorldColumn> = (column) => {
    const className = column.isAlive
        ? "population alive"
        : "population dead";
    
    return (
        <td>
            <div className={className}></div>
        </td>
    );
}

export default WorldGridColumn;
