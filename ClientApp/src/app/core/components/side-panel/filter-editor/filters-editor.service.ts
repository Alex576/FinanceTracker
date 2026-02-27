import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormEditorModel } from '../../../models/controls/form-editor-model';
import { FormModel } from '../../../models/form-editor/form-model';
import { FiltersEditorApiService } from './filters-editor-api.service';

@Injectable()
export class FiltersEditorService {
  private readonly api = inject(FiltersEditorApiService);

  getForm(model: FormEditorModel): Observable<FormModel> {
    return this.api.getForm(model);
  }
}
