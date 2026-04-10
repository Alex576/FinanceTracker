import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { tap } from 'rxjs';
import { OperationResult, OperationResultData } from '../../../models/operation-result/operation-result';
import { isSuccess } from '../../../models/operation-result/result-code';
import { DashboardItem } from '../../dashboard-panel/models/dashboard-item';
import { FormComponent } from "../../form/form.component";
import { BaseSidePanelComponent } from '../base-side-panel.component';
import { SidePanelViewComponent } from "../side-panel-view/side-panel-view.component";
import { CapitalEditorService } from './capital-editor.service';

@Component({
  selector: 'app-capital-editor',
  templateUrl: './capital-editor.component.html',
  styleUrls: ['./capital-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [SidePanelViewComponent, FormComponent, MatButtonModule],
  providers: [CapitalEditorService],
})
export class CapitalEditorComponent extends BaseSidePanelComponent {
  private readonly service = inject(CapitalEditorService);

  private get data(): number {
    return this.panelData.data as number;
  }

  constructor() {
    super();

    this.service.getForm({ itemId: this.data, tileCode: this.tileCode })
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed(),
      )
      .subscribe();
  }

  onFormChanged(): void {
    this.service.updateForm(this.getFormUpdateModel())
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  onDelete(): void {
    this.service.deleteItem({ id: this.data })
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: ({ code }: OperationResult) => {
          if (isSuccess(code)) {
            this.sidePanelService.close();
          }
        }
      });
  }

  onSave(): void {
    this.service.saveForm(this.getFormUpdateModel())
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: (result: OperationResultData<DashboardItem>) => {
          if (isSuccess(result.code)) {
            this.sidePanelService.close();
          }
        }
      });
  }
}
