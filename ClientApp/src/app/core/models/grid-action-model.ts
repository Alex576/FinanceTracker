import { RowAction } from "../components/ag-grid/models/row-action";
import { RowTag } from "../components/ag-grid/models/row-tag";

export interface GridActionModel {
    action: RowAction;
    data: RowTag;
}