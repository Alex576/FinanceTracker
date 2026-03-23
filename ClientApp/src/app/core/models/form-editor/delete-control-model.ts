import { TileCode } from "../tile-code";
import { EditorType } from "./editor-type";

export interface RemoveItemModel {
    tileCode: TileCode;
    itemId: string;
    type: EditorType;
}