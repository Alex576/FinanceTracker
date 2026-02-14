import { HttpContext, HttpContextToken } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RefreshTokenModel } from '../models/refresh-token-model';
import { BaseApiService } from '../services/base-api.service';

export const IS_REFRESH_TOKEN = new HttpContextToken<boolean>(() => false);
@Injectable({
  providedIn: 'root'
})
export class AuthorizationApiService extends BaseApiService {
  private readonly UPDATE_ACCESS_TOKEN = 'Authorization/RefreshToken';

  refreshToken(accessToken: string): Observable<RefreshTokenModel> {
    return this.http.post<RefreshTokenModel>(`${this.baseUrl}${this.UPDATE_ACCESS_TOKEN}`, { accessToken }, { withCredentials: true, context: new HttpContext().set(IS_REFRESH_TOKEN, true) });
  }
}
