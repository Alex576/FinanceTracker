import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from '../models/app-config';
import { BaseApiService } from '../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class AppApiService extends BaseApiService {
  private readonly GET_CONFIG = 'Configuration/GetConfig';

  getAppConfig(): Observable<AppConfig> {
    return this.http.get<AppConfig>(`${this.baseUrl}${this.GET_CONFIG}`);
  }
}
