import React from 'react';
import { WorldRow } from '../../models/WorldRow';
import WorldGridColumn from './world-grid-column/world-grid-column';

const WorldGridRow: React.FC<WorldRow> = (row) => {
    return (
        <tr>
            {row.columns.map(column => <WorldGridColumn isAlive={column.isAlive} />)}
        </tr>
    );
}

export default WorldGridRow;
