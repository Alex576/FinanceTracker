// import { Injectable } from '@angular/core';
// import { Observable } from 'rxjs';
// import { FormEditorModel } from '../../../models/form-editor/form-editor-model';
// import { FormModel } from '../../../models/form-editor/form-model';
// import { FormSaveModel } from '../../../models/form-editor/form-save-model';
// import { FormUpdateModel } from '../../../models/form-editor/form-update-model';
// import { OperationResultData } from '../../../models/operation-result/operation-result';
// import { BaseApiService } from '../../../services/base-api.service';
// import { LayoutEditorModel } from '../../layout-editor/models/layout-editor-model';

// @Injectable({
//   providedIn: 'root'
// })
// export class GridColumnEditorApiService extends BaseApiService {
//   private readonly GET_FORM = "GridEditor/GetForm";
//   private readonly UPDATER_FORM = "GridEditor/UpdateForm";
//   private readonly SAVE_FORM = "GridEditor/SaveForm";

//   getForm(model: FormEditorModel): Observable<FormModel> {
//     return this.post(this.GET_FORM, model);
//   }

//   updateForm(model: FormUpdateModel): Observable<FormModel> {
//     return this.post(this.UPDATER_FORM, model);
//   }

//   saveForm(model: FormSaveModel): Observable<OperationResultData<LayoutEditorModel>> {
//     return this.post(this.SAVE_FORM, model);
//   }
// }
