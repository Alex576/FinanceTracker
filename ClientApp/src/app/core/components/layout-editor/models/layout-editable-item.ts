import { ComboControlSettings } from "../../../models/controls/combo-control-settings";
import { ControlSettings } from "../../../models/controls/control-settings";
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
    type: TileTypeCode;
    tileCode: TileCode;
    data: any;//LayoutItemEntity;
}

export interface GridLayoutEntity extends LayoutEntityBase {

}

export interface FilterLayoutEntity extends LayoutEntityBase {
    filters: FilterControlEntity[];
}

export interface FilterControlEntity extends ControlEntityBase<ComboControlSettings> {
    objCode?: number;
    factName?: string;
}
export interface ControlEntityBase<TSettings extends ControlSettings> {
    name: string;
    tileItemCode: number;
    settings: TSettings;
}

export interface LayoutEntityBase {
    tileCode: TileCode;
}