import { TileCode } from "../tile-code";
import { EditorType } from "./editor-type";

export interface FormEditorModel {
    tileCode: TileCode;
    itemId: string;
    type: EditorType;
}