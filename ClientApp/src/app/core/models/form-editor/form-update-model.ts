import { FormControlValue } from "../controls/form-control-value";
import { TileCode } from "../tile-code";
import { EditorType } from "./editor-type";

export class FormUpdateModel {
    updatedControls: FormControlValue[] = [];

    constructor(
        public tileCode: TileCode,
        public itemId: string,
        public type: EditorType,
    ) { }
}