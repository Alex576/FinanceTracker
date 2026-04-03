import { Component, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatIconRegistry } from '@angular/material/icon';
import { RouterOutlet } from '@angular/router';
import { tap } from 'rxjs';
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
  private readonly service = inject(AppService);
  private readonly iconRegistry = inject(MatIconRegistry);

  protected readonly isLoaded = signal(false);

  protected readonly title = 'FinanceTracker';


  constructor() {
    this.themeSwitcherService.init();
    this.iconRegistry.setDefaultFontSetClass('material-symbols-outlined');

    this.loadConfig();
  }

  private loadConfig(): void {
    this.service.getAppConfig()
      .pipe(
        tap({
          next: () => {
            this.isLoaded.set(true);
          }
        }),
        takeUntilDestroyed()
      )
      .subscribe();
  }
}
