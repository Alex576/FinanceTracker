import { EditorType } from "../../../../models/form-editor/editor-type";
import { TileCode } from "../../../../models/tile-code";

export interface RemoveLayoutItemModel {
    tileCode: TileCode;
    itemId: string;
    type: EditorType;
}