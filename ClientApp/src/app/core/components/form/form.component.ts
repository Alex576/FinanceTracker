import { ChangeDetectionStrategy, Component, computed, inject, input, output } from '@angular/core';
import { FormControl } from '../../models/controls/form-control';
import { FormAction } from '../../models/form-editor/form-action';
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

  readonly formChanged = output<void>();

  // protected readonly actionCode = FormActionCode;

  private readonly service = inject(FormService);

  protected readonly controls = computed<FormControl[]>(() => this.form().controls);
  protected readonly actions = computed<FormAction[]>(() => this.form().actions);

  constructor() {
    // effect(() => {
    //   this.service.init(this.form());
    // });
  }

  onControlChanged(control: FormControl): void {
    this.service.updateControl(control);
    this.formChanged.emit();
  }

  // onSave(): void {
  //   this.onFormSave.emit(this.service.getFormUpdateModel(this.form()));
  // }
}
