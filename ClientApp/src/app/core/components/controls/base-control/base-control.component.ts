import { ChangeDetectionStrategy, Component, computed, effect, input, output, untracked } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl } from '@angular/forms';
import { FormControl as Control } from '../../../models/controls/form-control';

@Component({
  selector: 'app-base-control',
  templateUrl: './base-control.component.html',
  styleUrls: ['./base-control.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export abstract class BaseControlComponent<TData> {
  readonly control = input.required<Control>();

  readonly valueChanged = output<Control>();

  protected oldValue: TData;

  protected readonly formControl: FormControl<TData> = new FormControl(null, { updateOn: 'blur' });
  protected readonly controlValueChanged = toSignal(this.formControl.valueChanges);

  protected readonly name = computed<string>(() => this.control().name);
  protected readonly value = computed<TData>(() => this.control().value as TData);

  constructor() {
    effect(() => {
      const changedValue = this.controlValueChanged();
      if (changedValue != this.oldValue) {
        const control = untracked(() => this.control());
        control.value = changedValue;
        this.valueChanged.emit(control);
        this.oldValue = changedValue;
      }
    });

    effect(() => {
      const value = this.value();
      this.formControl.setValue(value, { emitEvent: false });
    });
  }

}
