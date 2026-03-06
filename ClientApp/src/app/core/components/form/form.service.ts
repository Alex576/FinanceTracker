import { Injectable } from '@angular/core';
import { FormControl } from '../../models/controls/form-control';

@Injectable()
export class FormService {

  // private form: FormModel;

  constructor() { }

  // init(form: FormModel): void {
  //   this.form = form;
  // }

  updateControl(control: FormControl): void {
    control.updated = true;
  }
}
