import { ChangeDetectionStrategy, Component, computed, inject, input } from '@angular/core';
import { FormControl } from '../../models/controls/form-control';
import { FormModel } from '../../models/form-editor/form-model';
import { ControlSwitchComponent } from "../controls/control-switch/control-switch.component";
import { FormService } from './form.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
  providers: [FormService],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ControlSwitchComponent]
})
export class FormComponent {
  readonly form = input.required<FormModel>();

  private readonly service = inject(FormService);

  protected readonly controls = computed<FormControl[]>(() => this.form().controls);
  constructor() { }


  onControlChanged(control: FormControl): void {
    console.log(control);
  }

}
