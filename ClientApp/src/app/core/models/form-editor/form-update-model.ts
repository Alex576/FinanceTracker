import { ChangedControlValue } from "../controls/changed-control-value";
import { TileCode } from "../tile-code";

export class FormUpdateModel {
    updatedControls: ChangedControlValue[] = [];

    constructor(
        public tileCode: TileCode,
        public itemId: string,
    ) { }
}