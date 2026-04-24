import { ControlType } from "../controls/control-type";
import { FormValueModel } from "../form-editor/form-value-model";
import { TileCode } from "../tile-code";

export interface FullScreenFormModel {
    formValueModel?: FormValueModel;
    tileCode: TileCode;
    controls?: ControlPreviewModel[];
}

export interface ControlPreviewModel {
    id: string;
    type: ControlType;
}