import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationResult } from '../../models/operation-result/operation-result';
import { UserModel } from '../../models/user-model';
import { BaseApiService } from '../../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class LoginApiService extends BaseApiService {
  private readonly LOGIN = 'Login/Login';

  login(login: string, password: string): Observable<OperationResult<UserModel>> {
    return this.http.post<OperationResult<UserModel>>(`${this.baseUrl}${this.LOGIN}`, { login, password }, { withCredentials: true });
  }

}
