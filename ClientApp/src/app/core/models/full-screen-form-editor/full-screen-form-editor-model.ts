import { FormControl } from "../controls/form-control";

export interface FullScreenFormEditorModel {
    controls: FormControl[];
    // tileCode: TileCode;
    components: FormComponents;
}

export interface FormComponents {
    inputs: InputPresetCode[];
    dropdowns: DropdownPresetCode[];
    buttons: ButtonPresetCode[];
    containers: ContainerPresetCode[];
}


export enum InputPresetCode {
    Text = 1,
    Email = 2,
    Password = 3,
    Phone = 4,
    Number = 5,
    Float = 6,
    Link = 7,
    Time = 8,
    DateTime = 9,
    Between = 10,
}

export enum DropdownPresetCode {
    SingleSelect = 1,
    MultiSelect = 2,
}

export enum ButtonPresetCode {
    Button = 1,
    ButtonIcon = 2,
    Icon = 3,
}

export enum ContainerPresetCode {
    Section = 1,
    Group = 2,
    RadioGroup = 3,
}