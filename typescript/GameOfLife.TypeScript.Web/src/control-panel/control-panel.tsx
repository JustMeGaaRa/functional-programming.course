import './control-panel.css';
import React from 'react';
import { PopulationPattern } from '../models';

type ControlPanelProps = {
    patterns: Array<PopulationPattern>;
    handleOnSelect: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    handleOnCreateClick: (event: any) => void;
    handleOnStartClick: (event: any) => void;
    handleOnStopClick: (event: any) => void;
    handleOnNameChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    handleOnWidthChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    handleOnHeightChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const ControlPanel: React.FC<ControlPanelProps> = (props) => {
    const newPatternName = '';
    const newPatternWidth = 10;
    const newPatternHeight = 10;

    return (
        <section className="control-panel">
            <section className="control-panel layout">
                <section>
                    <button onClick={props.handleOnStartClick}>
                        Start
                    </button>
                    <button onClick={props.handleOnStopClick}>
                        Stop
                    </button>
                </section>
                <select onChange={props.handleOnSelect}>
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
                    onChange={props.handleOnNameChange}
                />
                <input
                    type="number"
                    placeholder="Width"
                    min={1}
                    max={50}
                    defaultValue={newPatternWidth}
                    onChange={props.handleOnWidthChange}
                />
                <input
                    type="number"
                    placeholder="Height"
                    min={1}
                    max={50}
                    defaultValue={newPatternHeight}
                    onChange={props.handleOnHeightChange}
                />
                <button onClick={props.handleOnCreateClick}>
                    Create
                </button>

            </section>
        </section>
    );
}

export default ControlPanel;