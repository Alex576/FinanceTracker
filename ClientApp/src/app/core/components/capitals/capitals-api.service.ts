import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl } from '../../models/controls/form-control';
import { GetGridLayoutModel } from '../../models/get-grid-layout-model';
import { ToolCode } from '../../models/tool-code';
import { BaseApiService } from '../../services/base-api.service';
import { DashboardLayout } from '../dashboard-panel/models/dashboard-layout';

@Injectable({
  providedIn: 'root'
})
export class CapitalsApiService extends BaseApiService {
  private readonly GET_LAYOUT = 'Capital/GetCapitals';
  private readonly GET_FILTERS = 'Capital/GetFilters';

  constructor() {
    super();
  }

  getLayout(model: GetGridLayoutModel): Observable<DashboardLayout> {
    return this.post(this.GET_LAYOUT, model);
  }

  getFilters(toolCode: ToolCode): Observable<FormControl[]> {
    return this.post(this.GET_FILTERS, { toolCode });
  }
}
