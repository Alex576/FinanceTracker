import { inject, Injectable } from '@angular/core';
import { mergeMap, Observable, of, tap } from 'rxjs';
import { AuthorizationApiService } from '../api/authorization-api.service';
import { RefreshTokenModel } from '../models/refresh-token-model';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private readonly api = inject(AuthorizationApiService);
  public isRefreshingToken = false;

  constructor() { }

  refreshToken(accessToken: string): Observable<RefreshTokenModel> {
    return of(null)
      .pipe(
        tap({ next: () => this.isRefreshingToken = true }),
        mergeMap(() => this.api.refreshToken(accessToken)
          .pipe(
            tap({
              next: () => this.isRefreshingToken = false,
              error: () => this.isRefreshingToken = false,
              complete: () => this.isRefreshingToken = false,
            },),

          )),
      );
  }
}
