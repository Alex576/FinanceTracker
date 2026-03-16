import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { tap } from 'rxjs';
import { EditorType } from '../../../models/form-editor/editor-type';
import { GridColumn } from '../../../models/grid-editor/grid-column';
import { OperationResult, OperationResultData } from '../../../models/operation-result/operation-result';
import { isSuccess } from '../../../models/operation-result/result-code';
import { FormComponent } from '../../form/form.component';
import { LayoutEditorService } from '../../layout-editor/layout-editor.service';
import { LayoutEditorModel } from '../../layout-editor/models/layout-editor-model';
import { BaseSidePanelComponent } from '../base-side-panel.component';
import { FiltersEditorService } from '../filter-editor/filters-editor.service';
import { SidePanelViewComponent } from '../side-panel-view/side-panel-view.component';
import { GridColumnEditorService } from './grid-column-editor.service';

@Component({
  selector: 'app-grid-column-editor',
  templateUrl: './grid-column-editor.component.html',
  styleUrls: ['./grid-column-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [GridColumnEditorService, FiltersEditorService],
  imports: [SidePanelViewComponent, FormComponent, MatButtonModule],
})
export class GridColumnEditorComponent extends BaseSidePanelComponent {
  private readonly service = inject(FiltersEditorService);
  private readonly layoutService = inject(LayoutEditorService);

  private get data(): GridColumn {
    return this.panelData.data as GridColumn;
  }

  constructor() {
    super();

    this.service.getForm({ tileCode: this.panelData.tileCode, itemId: this.data.id, type: EditorType.Grid })
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed(),
      )
      .subscribe();
  }


  onFormChanged(): void {
    this.service.updateForm(this.getFormUpdateModel(this.data.id, EditorType.Grid))
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  onDelete(): void {
    this.service.deleteControl({ tileCode: this.panelData.tileCode, controlId: this.data.id })
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: ({ code }: OperationResult) => {
          if (isSuccess(code)) {
            this.layoutService.removeLayoutElement(this.panelData.tileCode, this.data.id);
            this.sidePanelService.close();
          }
        }
      });
  }

  onSave(): void {
    this.service.saveForm(this.getFormUpdateModel(this.data.id, EditorType.Grid))
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: (result: OperationResultData<LayoutEditorModel>) => {
          if (isSuccess(result.code)) {
            this.layoutService.applyEditorLayout(result.result);
            this.sidePanelService.close();
          }
        }
      });
  }
}
