import { ChangeDetectionStrategy, Component, computed, effect, input, OnInit, output, untracked } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormControl } from '@angular/forms';
import { ComboControlSettings } from '../../../../models/controls/combo-control-settings';
import { FormControl as Control } from '../../../../models/controls/form-control';
import { Item } from '../../../../models/controls/item';

@Component({
  selector: 'app-base-select',
  template: ``,
  styleUrls: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export abstract class BaseSelectComponent<TData> implements OnInit {
  readonly control = input.required<Control>();
  // readonly value = input.required<TData>();

  readonly valueChanged = output<TData>();
  private oldValue: TData;

  protected readonly formControl: FormControl<TData> = new FormControl();
  protected readonly controlValueChanged = toSignal(this.formControl.valueChanges);

  protected readonly comboSettings = computed<ComboControlSettings>(() => this.control().settings as ComboControlSettings);
  protected readonly name = computed<string>(() => this.control().name);
  protected readonly items = computed<Item[]>(() => this.comboSettings().items);
  protected readonly value = computed<TData>(() => this.control().value as TData);

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

  ngOnInit() {

  }
}
