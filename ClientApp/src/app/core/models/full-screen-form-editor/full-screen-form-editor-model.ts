import { FormControl } from "../controls/form-control";

export interface FullScreenFormEditorModel {
    controls: FormControl[];
    // tileCode: TileCode;
    components: FullScreenFormComponent[];
}

export interface FullScreenFormComponent extends FormControl {
    controlGroup: string;
}


export enum ControlPresetCode {
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
    SingleSelect = 101,
    MultiSelect = 102,
    Button = 201,
    ButtonIcon = 202,
    Icon = 203,
    Section = 301,
    Group = 302,
    RadioGroup = 303,
}