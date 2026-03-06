import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormEditorModel } from '../../../models/controls/form-editor-model';
import { DeleteControlModel } from '../../../models/form-editor/delete-control-model';
import { FormModel } from '../../../models/form-editor/form-model';
import { FormSaveModel } from '../../../models/form-editor/form-save-model';
import { FormUpdateModel } from '../../../models/form-editor/form-update-model';
import { OperationResult, OperationResultData } from '../../../models/operation-result/operation-result';
import { BaseApiService } from '../../../services/base-api.service';
import { LayoutEditorModel } from '../../layout-editor/models/layout-editor-model';

@Injectable({
  providedIn: 'root'
})
export class FiltersEditorApiService extends BaseApiService {
  private readonly GET_FORM = "FormEditor/GetForm";
  private readonly UPDATER_FORM = "FormEditor/UpdateForm";
  private readonly SAVE_FORM = "FormEditor/SaveForm";

  private readonly DELETE_CONTROL = "FormEditor/DeleteControl";

  getForm(model: FormEditorModel): Observable<FormModel> {
    return this.post(this.GET_FORM, model);
  }

  updateForm(model: FormUpdateModel): Observable<FormModel> {
    return this.post(this.UPDATER_FORM, model);
  }

  saveForm(model: FormSaveModel): Observable<OperationResultData<LayoutEditorModel>> {
    return this.post(this.SAVE_FORM, model);
  }

  deleteControl(model: DeleteControlModel): Observable<OperationResult> {
    return this.post(this.DELETE_CONTROL, model);
  }
}
