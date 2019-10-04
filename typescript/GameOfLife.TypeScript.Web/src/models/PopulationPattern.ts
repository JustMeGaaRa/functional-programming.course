export class World {
    constructor(
        public width: number,
        public height: number,
        public rows: Array<WorldRow>
    ) { }
}

export class WorldRow {
    constructor(
        public columns: Array<WorldColumn>
    ) { }
}

export class WorldColumn {
    constructor(
        public isAlive: boolean
    ) { }
}

export class PopulationPattern {
    constructor(
        public patternId: number,
        public name: string,
        public width: number,
        public height: number
    ) { }
}
