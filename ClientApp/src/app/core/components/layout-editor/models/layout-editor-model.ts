import { ComboControl } from "../../../models/controls/combo-control";
import { LayoutEntity } from "./layout-editable-item";

export interface LayoutEditorModel {
    tileFilter: ComboControl;
    layoutItems: LayoutEntity[];
}