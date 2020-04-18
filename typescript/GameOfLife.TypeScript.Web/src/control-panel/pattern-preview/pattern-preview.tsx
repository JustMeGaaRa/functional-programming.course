import './pattern-preview.css';
import * as React from 'react';
import { Card } from 'semantic-ui-react';

type PatternPreviewProps = {
    patternId: number;
    patternName: string;
    patternSummary: string;
}

const PatternPreview: React.FC<PatternPreviewProps> = (props) => {
    return (
        <Card
            header={props.patternName}
            meta="Pattern"
            description={props.patternSummary}
        />
    );
}

export default PatternPreview;