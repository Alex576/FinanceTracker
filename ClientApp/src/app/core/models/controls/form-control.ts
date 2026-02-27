import { ControlSettings } from "./control-settings";
import { ControlType } from "./control-type";

export interface FormControl {
    id: string;
    name: string;
    type: ControlType;
    value: unknown;
    settings: ControlSettings;
}