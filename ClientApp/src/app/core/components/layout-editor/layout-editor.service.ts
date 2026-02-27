import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToolCode } from '../../models/tool-code';
import { LayoutEditorApiService } from './layout-editor-api.service';
import { LayoutEditorModel } from './models/layout-editor-model';
import { LayoutManagementModel } from './models/layout-management-model';

@Injectable()
export class LayoutEditorService {
  private readonly api = inject(LayoutEditorApiService);

  public getLayoutManagement(): Observable<LayoutManagementModel> {
    return this.api.getLayoutManagement();
  }

  public getLayoutEditor(toolCode: ToolCode): Observable<LayoutEditorModel> {
    return this.api.getLayoutEditor(toolCode);
  }
}
