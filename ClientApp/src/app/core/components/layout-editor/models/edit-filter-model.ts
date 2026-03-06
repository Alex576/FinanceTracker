import { FormControl } from "../../../models/controls/form-control";
import { TileCode } from "../../../models/tile-code";

export interface EditFilterModel {
    control: FormControl;
    tileCode: TileCode;
}