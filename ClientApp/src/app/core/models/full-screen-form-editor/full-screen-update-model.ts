import { FormControl } from "../controls/form-control";
import { FormModel } from "../form-editor/form-model";

export interface FullScreenUpdateModel {
    controls: FormControl[];
    optionsForm: FormModel;
}