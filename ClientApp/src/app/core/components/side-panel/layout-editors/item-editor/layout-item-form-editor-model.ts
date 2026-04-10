import { EditorType } from "../../../../models/form-editor/editor-type";
import { FormValueModel } from "../../../../models/form-editor/form-value-model";
import { TileCode } from "../../../../models/tile-code";

export interface LayoutItemFormEditorModel {
    formValueModel?: FormValueModel;
    itemId: string;
    type: EditorType;
    tileCode: TileCode;
}