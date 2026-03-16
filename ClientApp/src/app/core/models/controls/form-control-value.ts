import { FormControl } from "./form-control";

export interface FormControlValue {
    controlId: string;
    value: unknown;
    updated: boolean;
}

export function getFormControlValues(controls: FormControl[]): FormControlValue[] {
    return controls.map((control) => ({ controlId: control.id, value: control.value, updated: control.updated }));
}