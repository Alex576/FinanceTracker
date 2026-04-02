import { FormControl } from "../../../models/controls/form-control";
import { TileCode } from "../../../models/tile-code";
import { PinPosition } from "../../ag-grid/models/column";
import { ColumnDataType } from "../../ag-grid/models/column-data-type";
import { Grid } from "../../ag-grid/models/grid";
import { DashboardLayout } from "../../dashboard-panel/models/dashboard-layout";
import { TileTypeCode } from "./tile-type-code";


export interface ColumnEntity {
    name: string;
    columnId: string;
    width?: number;
    editable: boolean;
    pinned: PinPosition;
    lockPinned: boolean;
    autoHeight: boolean;
    wrapText: boolean;
    sortable: boolean;
    maxWidth?: number;
    resizable: boolean;
    columnDataType: ColumnDataType;

}

export type LayoutItemEntity = FilterLayoutEntity | GridLayoutEntity | DashboardLayoutEntity;

export interface LayoutEntity {
    tileCode: TileCode;
    data: LayoutItemEntity;
}

export interface DashboardLayoutEntity extends LayoutEntityBase {
    type: TileTypeCode.Dashboard;
    dashboardLayout: DashboardLayout;
}
export interface GridLayoutEntity extends LayoutEntityBase {
    type: TileTypeCode.Grid;
    gridEditor: GridEditorEntity;
}

export interface GridEditorEntity {
    gridEntity: Grid;
}

export interface FilterLayoutEntity extends LayoutEntityBase {
    type: TileTypeCode.Filter;
    filters: FormControl[];
}

export interface LayoutEntityBase {
    tileCode: TileCode;
}