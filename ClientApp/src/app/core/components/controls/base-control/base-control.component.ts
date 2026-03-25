import { ChangeDetectionStrategy, Component, computed, effect, input, output, untracked } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl, Validators } from '@angular/forms';
import { ControlSettings } from '../../../models/controls/control-settings';
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
  readonly validChanged = output<Control>();

  protected oldValue: TData;

  protected readonly formControl: FormControl<TData> = new FormControl(null, { updateOn: 'blur' });
  protected readonly controlValueChanged = toSignal(this.formControl.valueChanges);
  protected readonly controlStatusChanged = toSignal(this.formControl.statusChanges);

  protected readonly isInvalid = computed<boolean>(() => this.controlStatusChanged() === 'INVALID');

  protected readonly settings = computed<ControlSettings>(() => this.control().settings);
  protected readonly name = computed<string>(() => this.control().name);
  protected readonly value = computed<TData>(() => this.control().value as TData);

  protected readonly editable = computed<boolean>(() => this.settings().editable);
  protected readonly required = computed<boolean>(() => this.settings().required);

  constructor() {
    effect(() => {
      const controlState = this.controlStatusChanged();
      const control = this.control();
      const prevState = control.settings.invalid;
      control.settings.invalid = controlState === 'INVALID';
      if (control.settings.invalid || prevState !== control.settings.invalid) {
        this.validChanged.emit(control);
      }
    });

    effect(() => {
      const control = untracked(() => this.control());
      const changedValue = this.controlValueChanged();
      if (changedValue != this.oldValue) {
        control.value = changedValue;
        this.valueChanged.emit(control);
        this.oldValue = changedValue;
      }
    });

    effect(() => {
      const value = this.value();
      this.oldValue = value;
      this.formControl.setValue(value, { emitEvent: false });
    });

    effect(() => {
      if (this.required()) {
        this.formControl.setValidators(Validators.required);
      } else {
        this.formControl.removeValidators(Validators.required);
      }
      this.formControl.updateValueAndValidity();
    });

    effect(() => {
      if (this.editable()) {
        this.formControl.enable();
      } else {
        this.formControl.disable();
      }
    });
  }

}
