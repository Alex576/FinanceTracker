import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FinancesApiService } from './finances-api.service';

@Injectable()
export class FinancesService {
  private readonly api = inject(FinancesApiService);

  constructor() { }

  getLayout(id: number): Observable<any> {
    return this.api.getLayout(id);
  }
}
