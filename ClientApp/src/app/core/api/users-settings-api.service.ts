import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SaveUserSettingsModel } from '../models/user-settings/save-user-settings-model';
import { UserSettingsModel } from '../models/user-settings/user-settings-model';
import { BaseApiService } from '../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class UsersSettingsApiService extends BaseApiService {
  private readonly GET_USER_SETTINGS = "UserSettings/GetUserSettings";
  private readonly GET_LAST_SESSION_SETTING = "UserSettings/GetLastSessionSetting";
  private readonly SAVE_USER_LAST_SESSION_SETTING = "UserSettings/SaveUserLastSessionSetting";

  public getSettings<T>(model: UserSettingsModel): Observable<T> {
    return this.post<T>(this.GET_USER_SETTINGS, model);
  }

  public getLastSessionSettings<T>(model: UserSettingsModel): Observable<T> {
    return this.post<T>(this.GET_LAST_SESSION_SETTING, model);
  }

  public saveUserLastSessionSetting(model: SaveUserSettingsModel): Observable<void> {
    return this.post(this.SAVE_USER_LAST_SESSION_SETTING, model);
  }
}
