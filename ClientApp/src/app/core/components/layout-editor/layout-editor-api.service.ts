import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToolCode } from '../../models/tool-code';
import { BaseApiService } from '../../services/base-api.service';
import { LayoutEditorModel } from './models/layout-editor-model';
import { LayoutManagementModel } from './models/layout-management-model';

@Injectable({
  providedIn: 'root'
})
export class LayoutEditorApiService extends BaseApiService {
  private readonly GET_LAYOUT_MANAGEMENT = 'Layout/GetLayoutManagement';
  private readonly GET_LAYOUT_EDITOR = 'Layout/GetLayoutEditor';

  public getLayoutManagement(): Observable<LayoutManagementModel> {
    return this.post(this.GET_LAYOUT_MANAGEMENT);
  }

  public getLayoutEditor(toolCode: ToolCode): Observable<LayoutEditorModel> {
    return this.post(this.GET_LAYOUT_EDITOR, { toolCode });
  }

}
