import { FormControl } from "../controls/form-control";
import { TileCode } from "../tile-code";
import { FormAction } from "./form-action";

export interface FormModel {
    controls: FormControl[];
    tileCode: TileCode;
    actions: FormAction[];
}