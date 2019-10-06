import { WorldRow } from "./WorldRow";

export class World {
    constructor(
        public generation: number,
        public width: number,
        public height: number,
        public rows: Array<WorldRow>
    ) { }
}
