import { Component, inject, signal } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { RouterOutlet } from '@angular/router';
import { LoadingComponent } from './core/components/loading/loading.component';
import { AppService } from './core/services/app.service';
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
  // private readonly service = inject(AppService);
  private readonly iconRegistry = inject(MatIconRegistry);
  // private readonly translateService = inject(TranslateService);

  protected readonly isLoaded = signal(false);

  constructor() {
    this.themeSwitcherService.init();
    this.iconRegistry.setDefaultFontSetClass('material-symbols-outlined');
    this.isLoaded.set(true);
    // this.loadConfig();
  }

  // private loadConfig(): void {
  //   forkJoin([this.service.getAppConfig(), this.translateService.loadTranslationsAsync(LanguageCode.EN)])
  //     .pipe(
  //       tap({
  //         next: () => {
  //           this.isLoaded.set(true);
  //         }
  //       }),
  //       takeUntilDestroyed()
  //     )
  //     .subscribe();
  // }
}
