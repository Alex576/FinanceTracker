import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormModel } from '../../models/form-editor/form-model';
import { OperationResult, OperationResultData } from '../../models/operation-result/operation-result';
import { BaseApiService } from '../../services/base-api.service';
import { LayoutEditorModel } from '../layout-editor/models/layout-editor-model';
import { LayoutItemFormEditorModel } from './layout-editors/item-editor/layout-item-form-editor-model';
import { RemoveLayoutItemModel } from './layout-editors/item-editor/remove-layout-item-model';

@Injectable({
  providedIn: 'root'
})
export class FiltersEditorApiService extends BaseApiService {
  private readonly GET_FORM = "LayoutItemEditor/GetForm";
  private readonly UPDATER_FORM = "LayoutItemEditor/UpdateForm";
  private readonly SAVE_FORM = "LayoutItemEditor/SaveForm";

  private readonly REMOVE_ITEM = "LayoutItemEditor/RemoveItem";

  getForm(model: LayoutItemFormEditorModel): Observable<FormModel> {
    return this.post(this.GET_FORM, model);
  }

  updateForm(model: LayoutItemFormEditorModel): Observable<FormModel> {
    return this.post(this.UPDATER_FORM, model);
  }

  saveForm(model: LayoutItemFormEditorModel): Observable<OperationResultData<LayoutEditorModel>> {
    return this.post(this.SAVE_FORM, model);
  }

  removeItem(model: RemoveLayoutItemModel): Observable<OperationResult> {
    return this.post(this.REMOVE_ITEM, model);
  }
}
