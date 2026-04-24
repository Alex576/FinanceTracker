import { FormValueModel } from "../../../../models/form-editor/form-value-model";
import { TileCode } from "../../../../models/tile-code";

export interface LayoutItemFormEditorModel {
    formValueModel?: FormValueModel;
    itemId: string;
    tileCode: TileCode;
}