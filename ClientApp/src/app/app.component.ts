import { Component, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterOutlet } from '@angular/router';
import { tap } from 'rxjs';
import { LoadingComponent } from './core/components/loading/loading.component';
import { LocalStorageKeys } from './core/models/local-storage-keys';
import { AppService } from './core/services/app.service';
import { StorageService } from './core/services/storage.service';
import { ThemeSwitcherService } from './core/services/theme-switcher.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [RouterOutlet, LoadingComponent],
  providers: [AppService],
})
export class AppComponent {
  private readonly themeSwitcherService = inject(ThemeSwitcherService);
  private readonly storageService = inject(StorageService);
  private readonly service = inject(AppService);

  protected readonly isLoaded = signal(true); //todo revert to false

  protected readonly title = 'FinanceTracker';
  private readonly darkThemeName = 'dark-theme';
  private readonly lightThemeName = 'light-theme';

  constructor() {
    this.service.getAppConfig()
      .pipe(
        tap({ next: () => this.isLoaded.set(true) }),
        takeUntilDestroyed(),
      )
      .subscribe();

    const savedTheme = this.storageService.getValue(LocalStorageKeys.Theme) ?? this.lightThemeName;
    document.body.classList.add(savedTheme);
    document.body.dataset['agThemeMode'] = savedTheme;

    this.themeSwitcherService.toggleTheme$
      .pipe(
        takeUntilDestroyed()
      )
      .subscribe({
        next: () => {
          let themeName: string = null;
          if (document.body.classList.toggle(this.darkThemeName))
            themeName = this.darkThemeName;
          if (document.body.classList.toggle(this.lightThemeName))
            themeName = this.lightThemeName;
          if (themeName) {
            this.storageService.saveValue(LocalStorageKeys.Theme, themeName);
            document.body.dataset['agThemeMode'] = themeName;
          }
        }
      });
  }
}
