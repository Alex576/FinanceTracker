import { ColumnDataType } from "./column-data-type";

export interface Column {
    field: string;
    columnId: number;
    columnDataType: ColumnDataType;
    width: number;
    editable: boolean;
    filter: boolean;
    pin: PinPosition;
    lockPin: boolean;
    autoHeight: boolean;
    wrapText: boolean;
    sortable: boolean;
    maxWidth: number;
    resizable: boolean;

}

export enum PinPosition {
    None = 0,
    Left = 1,
    Right = 2,
}