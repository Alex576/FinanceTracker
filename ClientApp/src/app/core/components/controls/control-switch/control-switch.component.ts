import { ChangeDetectionStrategy, Component, input, OnInit, output } from '@angular/core';
import { ControlType } from '../../../models/controls/control-type';
import { FormControl } from '../../../models/controls/form-control';
import { InputComponent } from "../input/input.component";
import { SelectSwitchComponent } from "../selectors/select-switch/select-switch.component";

@Component({
  selector: 'app-control-switch',
  templateUrl: './control-switch.component.html',
  styleUrls: ['./control-switch.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [InputComponent, SelectSwitchComponent]
})
export class ControlSwitchComponent implements OnInit {
  readonly control = input.required<FormControl>();

  readonly controlChanged = output<FormControl>();
  readonly validChanged = output<FormControl>();

  protected readonly controlType = ControlType;
  constructor() { }

  ngOnInit() {
  }

  onControlChanged(control: FormControl): void {
    this.controlChanged.emit(control);
  }

  onValidChanged(control: FormControl): void {
    this.validChanged.emit(control);
  }
}
