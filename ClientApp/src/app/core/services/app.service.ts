import { inject, Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { AppApiService } from '../api/app-api.service';
import { AppConfig } from '../models/app-config';

@Injectable()
export class AppService {
  private readonly api = inject(AppApiService);
  private _config: AppConfig;

  public get config(): AppConfig {
    return this._config;
  }

  getAppConfig(): Observable<void> {
    return this.api.getAppConfig()
      .pipe(
        tap({ next: (config) => this._config = config }),
        map(() => { return; }),
      );
  }

}
