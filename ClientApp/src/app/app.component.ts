import { Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterOutlet } from '@angular/router';
import { LocalStorageKeys } from './core/models/local-storage-keys';
import { ThemeSwitcherService } from './core/services/theme-switcher.service';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  private readonly themeSwitcherService = inject(ThemeSwitcherService);

  title = 'FinanceTracker';
  private readonly darkThemeName = 'dark-theme';
  private readonly lightThemeName = 'light-theme';
  // private readonly themeElement = viewChild<ElementRef<HTMLElement>>('themeElement');

  constructor() {
    // effect(() => {
    //   const themeElement = this.themeElement();
    //   if (!themeElement) return;

    const savedTheme = localStorage.getItem(LocalStorageKeys.Theme) ?? this.lightThemeName;
    document.body.classList.add(savedTheme);
    document.body.dataset['agThemeMode'] = savedTheme;

    // });.

    this.themeSwitcherService.toggleTheme$
      .pipe(
        takeUntilDestroyed()
      )
      .subscribe({
        next: () => {
          // const themeElement = this.themeElement();
          // if (!themeElement) return;

          let themeName: string = null;
          if (document.body.classList.toggle(this.darkThemeName))
            themeName = this.darkThemeName;
          if (document.body.classList.toggle(this.lightThemeName))
            themeName = this.lightThemeName;
          if (themeName) {
            localStorage.setItem(LocalStorageKeys.Theme, themeName);
            document.body.dataset['agThemeMode'] = themeName;
          }
        }
      });
  }
}
