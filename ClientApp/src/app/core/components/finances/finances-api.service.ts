import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseApiService } from '../../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class FinancesApiService extends BaseApiService {
  private readonly GET_LAYOUT = 'Finances/GetFinances';

  constructor() {
    super();
  }

  getLayout(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}${this.GET_LAYOUT}`, { params: { id: id } });
  }
}
