import { ControlSettings } from "./control-settings";
import { Item } from "./item";

export interface ComboControlSettings extends ControlSettings {
    allowMultiselect: boolean;
    items: Item[];
}