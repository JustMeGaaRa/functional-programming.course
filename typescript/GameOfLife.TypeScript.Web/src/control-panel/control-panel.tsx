import './control-panel.css';
import React from 'react';
import PatternPreview from './pattern-preview/pattern-preview';
import { PopulationPattern } from '../models';
import { Button, Dropdown, DropdownItemProps, Divider, DropdownProps } from 'semantic-ui-react';

type ControlPanelProps = {
    patterns: Array<PopulationPattern>;
    onSelect: (event: React.SyntheticEvent<HTMLElement, Event>, data: DropdownProps) => void;
    onCreateClick: (event: any, patternId: number) => void;
}

const ControlPanel: React.FC<ControlPanelProps> = (props) => {
    const options: DropdownItemProps[] = props.patterns.map(pattern => {
            return {
                key: pattern.patternId,
                value: pattern.patternId,
                text: pattern.name
            }
        });

    return (
        <section className="control-panel">
            <Dropdown
                fluid
                search
                selection
                placeholder="Select the pattern"
                options={options}
                onChange={props.onSelect}
            />
            <Button fluid primary compact content="Create" onClick={() => {}} />
            <Divider inverted section />
            {props.patterns.map(pattern => (
                <PatternPreview
                    patternId={pattern.patternId}
                    patternName={pattern.name}
                    patternSummary={pattern.name}
                />
            ))}
        </section>
    );
}

export default ControlPanel;