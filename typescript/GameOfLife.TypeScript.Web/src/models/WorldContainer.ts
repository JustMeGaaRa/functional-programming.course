import { World } from "./World";

export class WorldContainer {
    constructor(
        public instanceId: string,
        public name: string,
        public world: World,
        public startX: number,
        public startY: number
    ) { }
}
