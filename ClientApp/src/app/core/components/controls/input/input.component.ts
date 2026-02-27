import { ChangeDetectionStrategy, Component, computed, effect, input, output, untracked } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from '@angular/material/input';
import { FormControl as Control } from '../../../models/controls/form-control';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatFormFieldModule, MatInputModule, ReactiveFormsModule]
})
export class InputComponent {
  readonly control = input.required<Control>();

  readonly valueChanged = output<string>();

  private oldValue: string;
  protected readonly name = computed<string>(() => this.control().name);
  protected readonly value = computed<string>(() => this.control().value as string);


  protected readonly formControl: FormControl<string> = new FormControl<string>(null);
  protected readonly controlValueChanged = toSignal(this.formControl.valueChanges);

  constructor() {
    effect(() => {
      const changedValue = this.controlValueChanged();
      if (changedValue != this.oldValue) {
        untracked(() => this.control()).value = changedValue;
        this.valueChanged.emit(changedValue);
        this.oldValue = changedValue;
      }
    });

    effect(() => {
      const value = this.value();
      this.formControl.setValue(value, { emitEvent: false });
    });
  }


}
