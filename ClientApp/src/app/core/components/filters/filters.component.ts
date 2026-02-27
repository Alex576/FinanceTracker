import { ChangeDetectionStrategy, Component, input, OnInit, output } from '@angular/core';
import { ChangedControlValue } from '../../models/controls/changed-control-value';
import { ComboControl } from '../../models/controls/combo-control';
import { SelectSwitchComponent } from "../controls/selectors/select-switch/select-switch.component";

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [SelectSwitchComponent]
})
export class FiltersComponent implements OnInit {
  readonly filters = input.required<ComboControl[], ComboControl[] | ComboControl>({
    transform: (filers: ComboControl | ComboControl[]) => {
      if (Array.isArray(filers)) {
        return filers;
      }
      return [filers];
    }
  });

  readonly valueChanged = output<ChangedControlValue>();

  constructor() { }

  ngOnInit() {
  }

  onValueChanged(control: ChangedControlValue): void {
    this.valueChanged.emit(control);
  }
}
