import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl } from '../../models/controls/form-control';
import { GetGridLayoutModel } from '../../models/get-grid-layout-model';
import { ToolCode } from '../../models/tool-code';
import { DashboardLayout } from '../dashboard-panel/models/dashboard-layout';
import { CapitalsApiService } from './capitals-api.service';

@Injectable()
export class CapitalsService {
  private readonly api = inject(CapitalsApiService);

  constructor() { }

  getLayout(model: GetGridLayoutModel): Observable<DashboardLayout> {
    return this.api.getLayout(model);
  }

  getFilters(toolCode: ToolCode): Observable<FormControl[]> {
    return this.api.getFilters(toolCode);
  }
}
