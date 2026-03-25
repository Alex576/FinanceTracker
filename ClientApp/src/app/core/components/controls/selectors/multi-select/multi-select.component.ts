import { ChangeDetectionStrategy, Component, effect } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormField, MatLabel, MatOption, MatSelect } from "@angular/material/select";
import { isAnySelectedValidator } from '../../validators/is-any-selected';
import { BaseSelectComponent } from '../base-select/base-select.component';

@Component({
  selector: 'app-multi-select',
  templateUrl: './multi-select.component.html',
  styleUrls: ['./multi-select.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatSelect, MatLabel, MatFormField, MatOption, ReactiveFormsModule]
})
export class MultiSelectComponent extends BaseSelectComponent<number[]> {

  constructor() {
    super();

    effect(() => {
      if (this.required()) {
        this.formControl.addValidators(isAnySelectedValidator);
      } else {
        this.formControl.removeValidators(isAnySelectedValidator);
      }
      this.formControl.updateValueAndValidity();
    });
  }
}
