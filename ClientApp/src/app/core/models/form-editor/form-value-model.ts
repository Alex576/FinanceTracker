import { FormControlValue } from "../controls/form-control-value";
import { TileCode } from "../tile-code";

export class FormValueModel {
    updatedControls: FormControlValue[] = [];

    constructor(
        public tileCode: TileCode,
    ) { }
}