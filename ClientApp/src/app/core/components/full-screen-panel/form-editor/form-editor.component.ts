import {
  CdkDrag,
  CdkDragDrop,
  CdkDropList,
  copyArrayItem,
  moveItemInArray,
} from '@angular/cdk/drag-drop';
import { NgTemplateOutlet } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, effect, inject, linkedSignal, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from "@angular/material/icon";
import { tap } from 'rxjs';
import { FormControl } from '../../../models/controls/form-control';
import { FormModel } from '../../../models/form-editor/form-model';
import { FormValueModel } from '../../../models/form-editor/form-value-model';
import { FullScreenFormComponent } from '../../../models/full-screen-form-editor/full-screen-form-editor-model';
import { FullScreenFormModel } from '../../../models/full-screen-form-editor/full-screen-form-model';
import { FullScreenUpdateModel } from '../../../models/full-screen-form-editor/full-screen-update-model';
import { TranslatePipe } from "../../../pipes/translate.pipe";
import { ControlSwitchComponent } from "../../controls/control-switch/control-switch.component";
import { FormComponent } from '../../form/form.component';
import { LoadingComponent } from "../../loading/loading.component";
import { BaseFullScreenPanelComponent } from '../base-full-screen-panel.component';
import { FormEditorService } from './form-editor.service';

@Component({
  selector: 'app-form-editor',
  templateUrl: './form-editor.component.html',
  styleUrls: ['./form-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatButtonModule, LoadingComponent, NgTemplateOutlet, TranslatePipe, CdkDropList, CdkDrag, ControlSwitchComponent, MatIconModule, FormComponent],
  providers: [FormEditorService],
})
export class FormEditorComponent extends BaseFullScreenPanelComponent {
  private readonly service = inject(FormEditorService);
  protected readonly controls = linkedSignal<FormControl[]>(() => this.form()?.controls ?? []);
  private readonly presets = computed<FullScreenFormComponent[]>(() => this.form()?.components ?? []);
  protected readonly optionsForm = signal<FormModel>(null);

  protected readonly presetsGroup = computed<{ group: string, controls: FullScreenFormComponent[]; }[]>(() =>
    Object.entries(this.presets().reduce((acc, preset) => {
      if (!acc[preset.controlGroup]) {
        acc[preset.controlGroup] = [];
      }
      acc[preset.controlGroup].push(preset);
      return acc;
    },
      {} as Record<string, FullScreenFormComponent[]>
    )).map(([group, controls]) => ({ group, controls }))
  );

  protected readonly selectedControl = signal<FormControl>(null, { equal: (current, prev) => current?.id === prev?.id });

  constructor() {
    super();

    effect(() => {
      var selectedControl = this.selectedControl();
      if (!selectedControl) return;

      this.service.getOptionsForm(this.getFullScreenFormModel(selectedControl.id, this.controls()))
        .pipe(
          takeUntilDestroyed(this.destroyRef)
        )
        .subscribe({
          next: (result: FullScreenUpdateModel) => {
            this.optionsForm.set(result.optionsForm);
          }
        });

    });

    this.service.getForm({ tileCode: this.tileCode, formValueModel: null, controls: null })
      .pipe(
        takeUntilDestroyed(),
      )
      .subscribe({
        next: (form) => this.form.set(form)
      });
  }

  private getFullScreenFormModel(selectedControl: string, controls: FormControl[], optionsFormValues?: FormValueModel): FullScreenFormModel {
    return { selectedControl, tileCode: this.tileCode, controls, formValueModel: optionsFormValues };
  }

  isSelected(control: FormControl): boolean {
    return this.selectedControl()?.id === control.id;
  }

  onSave(): void {

  }

  onClose(): void {
    this.fullScreenPanelService.close();
  }

  onAddControl(controlDrag: CdkDragDrop<FormControl[], FormControl[] | FullScreenFormComponent[]>): void {
    if (controlDrag.previousContainer === controlDrag.container) {
      moveItemInArray(controlDrag.container.data, controlDrag.previousIndex, controlDrag.currentIndex);
    } else {
      copyArrayItem(
        controlDrag.previousContainer.data,
        controlDrag.container.data,
        controlDrag.previousIndex,
        controlDrag.currentIndex,
      );
    }
  }

  onRemove(control: FormControl): void {
    this.controls.update((controls) => controls.filter(x => x.id !== control.id));
  }

  onClickControl(control: FormControl): void {
    this.selectedControl.set(control);
  }

  onChangedOptionsForm(): void {
    this.service.updateOptionsForm(this.getFullScreenFormModel(this.selectedControl().id, this.controls(), this.getFormUpdateModel()))
      .pipe(
        tap({ next: () => this.selectedControl.update((control) => { control.updated = true; return control; }) }),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe({
        next: (result: FullScreenUpdateModel) => {
          this.optionsForm.set(result.optionsForm);
        }
      });
  }

  private getFormUpdateModel(): FormValueModel {
    const form = this.form();
    const model: FormValueModel = new FormValueModel(this.tileCode);
    for (let i = 0; i < form.controls.length; i++) {
      const control = form.controls[i];
      model.updatedControls.push({ controlId: control.id, value: control.value, updated: control.updated });
    }
    return model;
  }
}
