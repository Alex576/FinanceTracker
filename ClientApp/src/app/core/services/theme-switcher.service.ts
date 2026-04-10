import { DestroyRef, inject, Injectable } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Subject } from 'rxjs';
import { LocalStorageKeys } from '../models/local-storage-keys';
import { Constants } from '../utils/constants';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class ThemeSwitcherService {
  private readonly storageService = inject(StorageService);
  private readonly destroyRef = inject(DestroyRef);

  private readonly darkThemeName = 'dark-theme';
  private readonly lightThemeName = 'light-theme';
  private readonly themeList = [this.darkThemeName, this.lightThemeName];

  private readonly toggleThemeSub$ = new Subject<void>();
  private readonly toggleTheme$ = this.toggleThemeSub$.asObservable();

  private get currentTheme(): string {
    return this.storageService.getValue(LocalStorageKeys.Theme) ?? this.lightThemeName;
  }

  constructor() { }

  init(): void {
    this.setTheme(this.currentTheme, false);
    this.subscribeThemeSwitch();
    const useAutoThemeSwitch = this.storageService.getValue<boolean>(LocalStorageKeys.AutoThemeSwitch) ?? false;
    if (useAutoThemeSwitch) {
      this.setThemeByCurrentTime();
    }
  }

  private setThemeByCurrentTime(): void {
    const currentHours = new Date(Date.now()).getHours();
    const isNightTime = currentHours >= Constants.AutoThemeSwitchStartInHours || currentHours <= Constants.AutoThemeSwitchEndInHours;
    if (isNightTime) {
      this.setTheme(this.darkThemeName, false);
    } else {
      this.setTheme(this.lightThemeName, false);
    }
  }

  onToggleTheme(): void {
    this.toggleThemeSub$.next();
  }

  setAutoSwitchTheme(enable: boolean): void {
    if (enable) {
      this.setThemeByCurrentTime();
    }
    else {
      this.setTheme(this.currentTheme, false);
    }
    this.storageService.saveValue(LocalStorageKeys.AutoThemeSwitch, JSON.stringify(enable));
  }

  private subscribeThemeSwitch(): void {
    this.toggleTheme$
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe({
        next: () => {
          let themeName: string = null;
          if (document.body.classList.contains(this.darkThemeName))
            themeName = this.lightThemeName;
          else if (document.body.classList.contains(this.lightThemeName))
            themeName = this.darkThemeName;
          if (themeName) {
            this.setTheme(themeName);
          } else {
            this.setTheme(this.lightThemeName);
          }
        }
      });
  }

  private setTheme(themeName: string, saveTheme = true): void {
    if (saveTheme) {
      this.storageService.saveValue(LocalStorageKeys.Theme, themeName);
    }
    document.body.dataset['agThemeMode'] = themeName;
    document.body.classList.remove(...this.themeList);
    document.body.classList.add(themeName);
  }
}
