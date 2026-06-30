import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FullScreenFormEditorModel } from '../../models/full-screen-form-editor/full-screen-form-editor-model';
import { FullScreenFormModel } from '../../models/full-screen-form-editor/full-screen-form-model';
import { FullScreenUpdateModel } from '../../models/full-screen-form-editor/full-screen-update-model';
import { OperationResult } from '../../models/operation-result/operation-result';
import { ToolCode } from '../../models/tool-code';
import { BaseApiService } from '../../services/base-api.service';
import { RemoveLayoutItemModel } from '../side-panel/layout-editors/item-editor/remove-layout-item-model';
import { LayoutEditorModel } from './models/layout-editor-model';
import { LayoutManagementModel } from './models/layout-management-model';

@Injectable({
  providedIn: 'root'
})
export class LayoutEditorApiService extends BaseApiService {
  private readonly GET_LAYOUT_MANAGEMENT = 'Layout/GetLayoutManagement';
  private readonly GET_LAYOUT_EDITOR = 'Layout/GetLayoutEditor';
  private readonly REMOVE_ELEMENT = 'Layout/RemoveElement';
  private readonly GET_FORM = 'Layout/GetForm';
  private readonly GET_OPTIONS_FORM = 'Layout/GetOptionsForm';
  private readonly UPDATE_OPTIONS_FORM = 'Layout/UpdateOptionsForm';

  getLayoutManagement(): Observable<LayoutManagementModel> {
    return this.post(this.GET_LAYOUT_MANAGEMENT);
  }

  getLayoutEditor(toolCode: ToolCode): Observable<LayoutEditorModel> {
    return this.post(this.GET_LAYOUT_EDITOR, { toolCode });
  }

  removeLayoutItem(model: RemoveLayoutItemModel): Observable<OperationResult> {
    return this.post(this.REMOVE_ELEMENT, model);
  }

  getForm(model: FullScreenFormModel): Observable<FullScreenFormEditorModel> {
    return this.post(this.GET_FORM, model);
  }

  getOptionsForm(model: FullScreenFormModel): Observable<FullScreenUpdateModel> {
    return this.post(this.GET_OPTIONS_FORM, model);
  }

  updateOptionsForm(model: FullScreenFormModel): Observable<FullScreenUpdateModel> {
    return this.post(this.UPDATE_OPTIONS_FORM, model);
  }
}
