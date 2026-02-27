import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormEditorModel } from '../../../models/controls/form-editor-model';
import { FormModel } from '../../../models/form-editor/form-model';
import { BaseApiService } from '../../../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class FiltersEditorApiService extends BaseApiService {
  private readonly GET_FORM = "FormEditor/GetForm";

  getForm(model: FormEditorModel): Observable<FormModel> {
    return this.post(this.GET_FORM, model);
  }
}
