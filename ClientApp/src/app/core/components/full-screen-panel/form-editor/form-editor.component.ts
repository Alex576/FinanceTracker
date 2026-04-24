import { NgTemplateOutlet } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { TranslatePipe } from "../../../pipes/translate.pipe";
import { LoadingComponent } from "../../loading/loading.component";
import { BaseFullScreenPanelComponent } from '../base-full-screen-panel.component';
import { FormEditorService } from './form-editor.service';

@Component({
  selector: 'app-form-editor',
  templateUrl: './form-editor.component.html',
  styleUrls: ['./form-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatButtonModule, LoadingComponent, NgTemplateOutlet, TranslatePipe],
  providers: [FormEditorService],
})
export class FormEditorComponent extends BaseFullScreenPanelComponent {
  private readonly service = inject(FormEditorService);
  constructor() {
    super();

    this.service.getForm({ tileCode: this.tileCode, formValueModel: null, controls: null })
      .pipe(
        takeUntilDestroyed(),
      )
      .subscribe({
        next: (form) => this.form.set(form)
      });
  }

  onSave(): void {

  }

  onClose(): void {
    this.fullScreenPanelService.close();
  }
}
