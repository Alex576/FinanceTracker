import { FormControl } from "../../../models/controls/form-control";
import { TileCode } from "../../../models/tile-code";
import { PinPosition } from "../../ag-grid/models/column";
import { TileTypeCode } from "./tile-type-code";

// export interface LayoutEditableItem {
//     tileCode: TileCode;
//     itemType: ItemType;
// }

export interface GridLayoutEntity extends LayoutEntityBase {
    columns: ColumnEntity[];
}

export interface ColumnEntity {
    name: string;
    columnId: number;
    width?: number;
    editable: boolean;
    pinned: PinPosition;
    lockPinned: boolean;
    autoHeight: boolean;
    wrapText: boolean;
    sortable: boolean;
    maxWidth?: number;
    resizable: boolean;

}

export type LayoutItemEntity = FilterLayoutEntity | GridLayoutEntity;

export interface LayoutEntity {
    tileCode: TileCode;
    data: LayoutItemEntity;
}

export interface GridLayoutEntity extends LayoutEntityBase {
    type: TileTypeCode.Grid;
}

export interface FilterLayoutEntity extends LayoutEntityBase {
    type: TileTypeCode.Filter;
    filters: FormControl[];
}

export interface LayoutEntityBase {
    tileCode: TileCode;
}