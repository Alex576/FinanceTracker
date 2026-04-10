import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormEditorModel } from '../../../models/form-editor/form-editor-model';
import { FormModel } from '../../../models/form-editor/form-model';
import { FormRemoveItem } from '../../../models/form-editor/form-remove-item';
import { FormSaveModel } from '../../../models/form-editor/form-save-model';
import { FormValueModel } from '../../../models/form-editor/form-value-model';
import { OperationResult, OperationResultData } from '../../../models/operation-result/operation-result';
import { BaseApiService } from '../../../services/base-api.service';
import { DashboardItem } from '../../dashboard-panel/models/dashboard-item';

@Injectable({
  providedIn: 'root'
})
export class CapitalEditorApiService extends BaseApiService {
  private readonly GET_FORM = "Capital/GetForm";
  private readonly UPDATER_FORM = "Capital/UpdateForm";
  private readonly SAVE_FORM = "Capital/SaveForm";

  private readonly REMOVE_ITEM = "Capital/RemoveItem";

  getForm(model: FormEditorModel): Observable<FormModel> {
    return this.post(this.GET_FORM, model);
  }

  updateForm(model: FormValueModel): Observable<FormModel> {
    return this.post(this.UPDATER_FORM, model);
  }

  saveForm(model: FormSaveModel): Observable<OperationResultData<DashboardItem>> {
    return this.post(this.SAVE_FORM, model);
  }

  removeItem(model: FormRemoveItem): Observable<OperationResult> {
    return this.post(this.REMOVE_ITEM, model);
  }
}
