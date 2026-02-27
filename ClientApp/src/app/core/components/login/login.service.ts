import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { OperationResult } from '../../models/operation-result/operation-result';
import { isError, isSuccess } from '../../models/operation-result/result-code';
import { UserModel } from '../../models/user-model';
import { NavigationService } from '../../services/navigation.service';
import { NotificationService } from '../../services/notification.service';
import { ToolbarService } from '../toolbar/toolbar.service';
import { LoginApiService } from './login-api.service';

@Injectable()
export class LoginService {
  private readonly api = inject(LoginApiService);
  private readonly toolbar = inject(ToolbarService);
  private readonly notificationService = inject(NotificationService);
  private readonly navigationService = inject(NavigationService);

  login(login: string, password: string): Observable<void> {
    return this.api.login(login, password)
      .pipe(
        map(({ result, code, description }: OperationResult<UserModel>) => {
          if (isError(code)) {
            this.notificationService.notifyError(description || 'Wrong user name or password');
          }
          if (isSuccess(code)) {
            this.notificationService.notify(description || 'Login success');
            this.toolbar.setUser(result);
            this.navigationService.navigateDefaultOrReturnUrl();
          }
          return;
        })
      );
  }

  logout(id: number): Observable<void> {
    return this.api.logout(id);
  }
}
