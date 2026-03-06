import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResultData } from '../../models/operation-result/operation-result';
import { UserModel } from '../../models/user-model';
import { BaseApiService } from '../../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class LoginApiService extends BaseApiService {
  private readonly LOGIN = 'Login/Login';
  private readonly LOGOUT = 'Login/Logout';

  login(login: string, password: string): Observable<OperationResultData<UserModel>> {
    return this.http.post<OperationResultData<UserModel>>(`${this.baseUrl}${this.LOGIN}`, { login, password }, { withCredentials: true });
  }

  logout(id: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.LOGOUT}`, { id });
  }
}
