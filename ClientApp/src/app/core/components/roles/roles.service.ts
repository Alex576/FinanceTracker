import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Grid } from '../ag-grid/models/grid';
import { RolesApiService } from './roles-api.service';

@Injectable()
export class RolesService {
  private readonly api = inject(RolesApiService);

  public getRolesGrid(): Observable<Grid> {
    return this.api.getRolesGrid();
  }
}
