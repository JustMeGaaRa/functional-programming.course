import { WorldColumn } from "./WorldColumn";

export class WorldRow {
    constructor(
        public number: number,
        public columns: Array<WorldColumn>
    ) { }
}
