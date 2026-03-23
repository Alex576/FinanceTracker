import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { RemoveItemModel } from '../../models/form-editor/delete-control-model';
import { FormEditorModel } from '../../models/form-editor/form-editor-model';
import { FormModel } from '../../models/form-editor/form-model';
import { FormSaveModel } from '../../models/form-editor/form-save-model';
import { FormUpdateModel } from '../../models/form-editor/form-update-model';
import { OperationResult, OperationResultData } from '../../models/operation-result/operation-result';
import { isSuccess, ResultCode } from '../../models/operation-result/result-code';
import { NotificationService } from '../../services/notification.service';
import { LayoutEditorModel } from '../layout-editor/models/layout-editor-model';
import { FiltersEditorApiService } from './layout-item-editor-api.service';

@Injectable()
export class LayoutItemEditorService {
  private readonly api = inject(FiltersEditorApiService);
  private readonly notify = inject(NotificationService);

  getForm(model: FormEditorModel): Observable<FormModel> {
    return this.api.getForm(model);
  }

  updateForm(model: FormUpdateModel): Observable<FormModel> {
    return this.api.updateForm(model);
  }

  saveForm(model: FormSaveModel): Observable<OperationResultData<LayoutEditorModel>> {
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

  deleteItem(model: RemoveItemModel): Observable<OperationResult> {
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
