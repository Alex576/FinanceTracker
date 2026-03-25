import { AbstractControl, ValidationErrors } from "@angular/forms";

export function isAnySelectedValidator(control: AbstractControl): ValidationErrors | null {
    return Array.isArray(control.value) && control.value.length === 0 ?
        { 'isAnySelected': { value: control.value } } :
        null;
}