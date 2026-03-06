import { ChangeDetectionStrategy, Component, input, OnInit, output } from '@angular/core';
import { ComboControl } from '../../models/controls/combo-control';
import { FormControl } from '../../models/controls/form-control';
import { SelectSwitchComponent } from "../controls/selectors/select-switch/select-switch.component";

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [SelectSwitchComponent]
})
export class FiltersComponent implements OnInit {
  readonly filters = input.required<ComboControl[], ComboControl[] | ComboControl | null>({
    transform: (filers: ComboControl | ComboControl[] | null) => {
      if (!filers) {
        return [];
      }
      if (Array.isArray(filers)) {
        return filers;
      }
      return [filers];
    }
  });

  readonly valueChanged = output<FormControl>();

  constructor() { }

  ngOnInit() {
  }

  onValueChanged(control: FormControl): void {
    this.valueChanged.emit(control);
  }
}
