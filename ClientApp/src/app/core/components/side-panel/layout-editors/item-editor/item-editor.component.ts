import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { tap } from 'rxjs';
import { OperationResult, OperationResultData } from '../../../../models/operation-result/operation-result';
import { isSuccess } from '../../../../models/operation-result/result-code';
import { FormComponent } from "../../../form/form.component";
import { LayoutEditorService } from '../../../layout-editor/layout-editor.service';
import { ItemFormDataModel } from '../../../layout-editor/models/item-form-data-model';
import { LayoutEditorModel } from '../../../layout-editor/models/layout-editor-model';
import { LayoutItemEditorService } from '../../layout-item-editor.service';
import { SidePanelViewComponent } from "../../side-panel-view/side-panel-view.component";
import { BaseLayoutItemSidePanelComponent } from '../base-layout-item-side-panel.component';

@Component({
  selector: 'app-item-editor',
  templateUrl: './item-editor.component.html',
  styleUrls: ['./item-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [SidePanelViewComponent, FormComponent, MatButtonModule],
  providers: [LayoutItemEditorService],
})
export class ItemEditorComponent extends BaseLayoutItemSidePanelComponent {
  private readonly service = inject(LayoutItemEditorService);
  private readonly layoutService = inject(LayoutEditorService);

  private get data(): ItemFormDataModel {
    return this.panelData.data as ItemFormDataModel;
  }

  constructor() {
    super();

    this.service.getForm({ tileCode: this.panelData.tileCode, itemId: this.data.itemId, type: this.data.editorType })
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed(),
      )
      .subscribe();
  }

  onFormChanged(): void {
    this.service.updateForm(this.getLayoutItemFormUpdateModel(this.data.itemId, this.data.editorType))
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  onDelete(): void {
    this.service.deleteItem({ tileCode: this.panelData.tileCode, itemId: this.data.itemId, type: this.data.editorType })
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: ({ code }: OperationResult) => {
          if (isSuccess(code)) {
            this.layoutService.removeLayoutElement(this.panelData.tileCode, this.data.itemId);
            this.sidePanelService.close();
          }
        }
      });
  }

  onSave(): void {
    this.service.saveForm(this.getLayoutItemFormUpdateModel(this.data.itemId, this.data.editorType))
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
