import React from 'react';
import './world-grid.css';
import { World } from '../models/PopulationPattern';
import WorldGridRow from './world-grid-row/world-grid-row';

const WorldGrid: React.FC<World> = (pattern) => {
    return (
        <div className="grid centered">
            <table>
                <tbody>
                    {pattern.rows.map(row => <WorldGridRow columns={row.columns} />)}
                </tbody>
            </table>
        </div>
    );
}

export default WorldGrid;
