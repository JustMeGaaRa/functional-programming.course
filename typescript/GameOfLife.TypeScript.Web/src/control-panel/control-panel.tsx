import React from 'react';
import './control-panel.css';

type ControlPanelProps = {

}

const ControlPanel: React.FC<ControlPanelProps> = (props) => {
    const newPatternName = '';
    const newPatternWidth = 10;
    const newPatternHeight = 10;
    
    // this.handleOnPatternCellClick = this.handleOnPatternCellClick.bind(this);
    // this.handleOnCreateClick = this.handleOnCreateClick.bind(this);
    // this.handleOnStartClick = this.handleOnStartClick.bind(this);
    // this.handleOnStopClick = this.handleOnStopClick.bind(this);
    // this.handleOnNameChange = this.handleOnNameChange.bind(this);
    // this.handleOnWidthChange = this.handleOnWidthChange.bind(this);
    // this.handleOnHeightChange = this.handleOnHeightChange.bind(this);

    return (
        <div className="control-panel-layout">
            <section>
                <button
                    //onClick={this.handleOnStartClick}
                >
                    Start
                </button>
                <button
                    //onClick={this.handleOnStopClick}
                >
                    Stop
                </button>
            </section>
            <select
                //onChange={this.handleOnSelect.bind(this)}
                >
                {/* {patterns.map(pattern => (
                    <option key={pattern.patternId} value={pattern.patternId}>
                    {pattern.name}
                    </option>
                ))} */}
            </select>
            <input
                type="text"
                placeholder="Name"
                minLength={3}
                maxLength={30}
                defaultValue={newPatternName}
                //onChange={this.handleOnNameChange}
            />
            <input
                type="number"
                placeholder="Width"
                min={1}
                max={50}
                defaultValue={newPatternWidth}
                //onChange={this.handleOnWidthChange}
            />
            <input
                type="number"
                placeholder="Height"
                min={1}
                max={50}
                defaultValue={newPatternHeight}
                //onChange={this.handleOnHeightChange}
            />
            <button
                //onClick={this.handleOnCreateClick}
            >
                Create
            </button>
        </div>
    );
}

export default ControlPanel;