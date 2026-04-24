import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { FormModel } from '../../models/form-editor/form-model';
import { OperationResult, OperationResultData } from '../../models/operation-result/operation-result';
import { isSuccess, ResultCode } from '../../models/operation-result/result-code';
import { NotificationService } from '../../services/notification.service';
import { LayoutEditorModel } from '../layout-editor/models/layout-editor-model';
import { LayoutItemFormEditorModel } from './layout-editors/item-editor/layout-item-form-editor-model';
import { RemoveLayoutItemModel } from './layout-editors/item-editor/remove-layout-item-model';
import { LayoutItemEditorApiService } from './layout-item-editor-api.service';

@Injectable()
export class LayoutItemEditorService {
  private readonly api = inject(LayoutItemEditorApiService);
  private readonly notify = inject(NotificationService);

  getForm(model: LayoutItemFormEditorModel): Observable<FormModel> {
    return this.api.getForm(model);
  }

  updateForm(model: LayoutItemFormEditorModel): Observable<FormModel> {
    return this.api.updateForm(model);
  }

  saveForm(model: LayoutItemFormEditorModel): Observable<OperationResultData<LayoutEditorModel>> {
    return this.api.saveForm(model)
      .pipe(
        tap({
          next: ({ code, description }: OperationResultData<LayoutEditorModel>) => {
            if (isSuccess(code)) {
              this.notify.notify(description ?? ResultCode[code].toString());
            }
          }
        })
      );
  }

  deleteItem(model: RemoveLayoutItemModel): Observable<OperationResult> {
    return this.api.removeItem(model)
      .pipe(
        tap({
          next: ({ code, description }: OperationResult) => {
            if (isSuccess(code)) {
              this.notify.notify(description ?? ResultCode[code].toString());
            }
          }
        })
      );
  }
}
