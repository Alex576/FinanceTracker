import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FullScreenFormEditorModel } from '../../../models/full-screen-form-editor/full-screen-form-editor-model';
import { FullScreenFormModel } from '../../../models/full-screen-form-editor/full-screen-form-model';
import { LayoutEditorApiService } from '../../layout-editor/layout-editor-api.service';

@Injectable()
export class FormEditorService {
  private readonly api = inject(LayoutEditorApiService);
  constructor() { }

  getForm(model: FullScreenFormModel): Observable<FullScreenFormEditorModel> {
    return this.api.getForm(model);
  }
}
