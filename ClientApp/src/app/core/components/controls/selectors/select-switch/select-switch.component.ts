import { ChangeDetectionStrategy, Component, computed, input, output } from '@angular/core';
import { ComboControlSettings } from '../../../../models/controls/combo-control-settings';
import { FormControl } from '../../../../models/controls/form-control';
import { MultiSelectComponent } from "../multi-select/multi-select.component";
import { SingleSelectComponent } from "../single-select/single-select.component";

@Component({
  selector: 'app-select-switch',
  templateUrl: './select-switch.component.html',
  styleUrls: ['./select-switch.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MultiSelectComponent, SingleSelectComponent]
})
export class SelectSwitchComponent {
  readonly control = input.required<FormControl>();

  readonly valueChanged = output<FormControl>();

  protected readonly comboSettings = computed<ComboControlSettings>(() => this.control().settings as ComboControlSettings);
  protected readonly isMulti = computed<boolean>(() => this.comboSettings().allowMultiselect);
  // protected readonly controlMultiValue = computed<number[]>(() => this.control().value as number[]);
  // protected readonly controlSingleValue = computed<number>(() => this.control().value as number);

  onValueChanged(control: FormControl): void {
    this.valueChanged.emit(control);
  }
}
