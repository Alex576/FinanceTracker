import { DestroyRef, inject, Injectable } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Observable, of, tap } from 'rxjs';
import { UsersSettingsApiService } from '../api/users-settings-api.service';
import { ToolbarService } from '../components/toolbar/toolbar.service';
import { LastSessionSetting } from '../models/user-settings/last-session-setting';
import { UserSetting } from '../models/user-settings/user-setting';
import { UserSettingsModel } from '../models/user-settings/user-settings-model';

@Injectable({
  providedIn: 'root'
})
export class UserSettingsService {
  private readonly api = inject(UsersSettingsApiService);
  private readonly toolbarService = inject(ToolbarService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly settingsMap = new Map<string, UserSetting>;

  public getSettings<T extends UserSetting>(model: UserSettingsModel): Observable<T> {
    return this.getOrLoadSettings<T>(model, (model) => this.api.getSettings(model));
  }

  public getLastSessionSettings<T extends LastSessionSetting>(model: UserSettingsModel): Observable<T> {
    return this.getOrLoadSettings<T>(model, (model) => this.api.getLastSessionSettings(model));
  }

  public getLoadedSettings<T extends UserSetting>(model: UserSettingsModel): T {
    const data = this.settingsMap.get(this.getKey(model));
    if (!data) {
      console.error(`Failed to get loaded settings setting code = ${model.settingCode}, tool code = ${model.toolCode}, tile code = ${model.tileCode}`);
    }
    return data as T;
  }

  public saveUserLastSessionSettingAsync<T extends UserSetting>(model: UserSettingsModel, value: T): void {
    this.settingsMap.set(this.getKey(model), value);
    this.api.saveUserLastSessionSetting({ ...model, value })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe();
  }

  private getOrLoadSettings<T extends UserSetting>(
    model: UserSettingsModel,
    loadFunc: <T extends UserSetting>(model: UserSettingsModel) => Observable<T>
  ): Observable<T> {
    const key = this.getKey(model);
    let settings = this.settingsMap.get(key);
    if (settings) {
      return of(settings as T);
    }
    return loadFunc<T>(model)
      .pipe(
        tap({ next: (data) => this.settingsMap.set(key, data ?? {}) })
      );
  }

  private getKey({ toolCode = 0, tileCode = 0, settingCode }: UserSettingsModel): string {
    return `${toolCode}${tileCode}${settingCode}`;
  }
}
