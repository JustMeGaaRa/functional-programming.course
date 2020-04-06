import './control-panel.css';
import React from 'react';
import { PopulationPattern } from '../models';

type ControlPanelProps = {
    patterns: Array<PopulationPattern>;
    onSelect: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    onCreateClick: (event: any) => void;
    onStartClick: (event: any) => void;
    onStopClick: (event: any) => void;
    onNameChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onWidthChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onHeightChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const ControlPanel: React.FC<ControlPanelProps> = (props) => {
    const newPatternName = '';
    const newPatternWidth = 10;
    const newPatternHeight = 10;

    return (
        <section className="control-panel">
            <section className="control-panel layout">
                <section>
                    <button onClick={props.onStartClick}>
                        Start
                    </button>
                    <button onClick={props.onStopClick}>
                        Stop
                    </button>
                </section>
                <select onChange={props.onSelect}>
                    {props.patterns.map(pattern => (
                        <option key={pattern.patternId} value={pattern.patternId}>
                        {pattern.name}
                        </option>
                    ))}
                </select>
                <input
                    type="text"
                    placeholder="Name"
                    minLength={3}
                    maxLength={30}
                    defaultValue={newPatternName}
                    onChange={props.onNameChange}
                />
                <input
                    type="number"
                    placeholder="Width"
                    min={1}
                    max={50}
                    defaultValue={newPatternWidth}
                    onChange={props.onWidthChange}
                />
                <input
                    type="number"
                    placeholder="Height"
                    min={1}
                    max={50}
                    defaultValue={newPatternHeight}
                    onChange={props.onHeightChange}
                />
                <button onClick={props.onCreateClick}>
                    Create
                </button>

            </section>
        </section>
    );
}

export default ControlPanel;