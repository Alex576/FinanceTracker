import { Row } from "./row";

export class UpdateGridModel {
    rowIndex: number;

    constructor(
        public add: Row[] = [],
        public update: Row[] = [],
        public remove: string[] = [],
    ) { }
}