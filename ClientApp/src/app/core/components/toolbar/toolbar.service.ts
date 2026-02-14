import { inject, Injectable } from '@angular/core';
import { LocalStorageKeys } from '../../models/local-storage-keys';
import { UserModel } from '../../models/user-model';
import { NavigationService } from '../../services/navigation.service';
import { StorageService } from '../../services/storage.service';

@Injectable({
  providedIn: 'root'
})
export class ToolbarService {
  private readonly storageService = inject(StorageService);
  private readonly navigationService = inject(NavigationService);

  private _currentUser: UserModel;

  public get currentUser(): UserModel {
    return this._currentUser;
  }

  constructor() {
    this._currentUser = this.storageService.getValue<UserModel>(LocalStorageKeys.CurrentUser);
    if (!this._currentUser) {
      this.navigationService.navigateToLoginPage();
    }
  }

  setUser(user: UserModel) {
    this._currentUser = user;
    this.storageService.saveValue(LocalStorageKeys.CurrentUser, JSON.stringify(user));
    this.storageService.saveValue(LocalStorageKeys.Token, user.accessToken);
  }
}
