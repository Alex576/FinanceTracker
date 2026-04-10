import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { FormEditorModel } from '../../../models/form-editor/form-editor-model';
import { FormModel } from '../../../models/form-editor/form-model';
import { FormRemoveItem } from '../../../models/form-editor/form-remove-item';
import { FormSaveModel } from '../../../models/form-editor/form-save-model';
import { FormValueModel } from '../../../models/form-editor/form-value-model';
import { OperationResult, OperationResultData } from '../../../models/operation-result/operation-result';
import { isSuccess, ResultCode } from '../../../models/operation-result/result-code';
import { NotificationService } from '../../../services/notification.service';
import { DashboardItem } from '../../dashboard-panel/models/dashboard-item';
import { CapitalEditorApiService } from './capital-editor-api.service';

@Injectable()
export class CapitalEditorService {

  private readonly api = inject(CapitalEditorApiService);
  private readonly notify = inject(NotificationService);

  getForm(model: FormEditorModel): Observable<FormModel> {
    return this.api.getForm(model);
  }

  updateForm(model: FormValueModel): Observable<FormModel> {
    return this.api.updateForm(model);
  }

  saveForm(model: FormSaveModel): Observable<OperationResultData<DashboardItem>> {
    return this.api.saveForm(model)
      .pipe(
        tap({
          next: ({ code, description }: OperationResultData<DashboardItem>) => {
            if (isSuccess(code)) {
              this.notify.notify(description ?? ResultCode[code].toString());
            }
          }
        })
      );
  }

  deleteItem(model: FormRemoveItem): Observable<OperationResult> {
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
