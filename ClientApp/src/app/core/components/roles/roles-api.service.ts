import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from '../../services/base-api.service';
import { Grid } from '../ag-grid/models/grid';

@Injectable({
  providedIn: 'root'
})
export class RolesApiService extends BaseApiService {
  private readonly GET_ROLES = 'Role/GetAllRolesGrid';

  public getRolesGrid(): Observable<Grid> {
    return this.http.get<Grid>(`${this.baseUrl}${this.GET_ROLES}`);
  }
}
