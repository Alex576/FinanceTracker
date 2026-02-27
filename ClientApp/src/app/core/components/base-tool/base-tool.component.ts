import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { RouteData } from '../../models/route-data';
import { ToolCode } from '../../models/tool-code';
import { UserSettingsModel } from '../../models/user-settings/user-settings-model';
import { UserSettingsService } from '../../services/user-settings.service';

@Component({
  selector: 'app-base-tool',
  template: ``,
  styleUrls: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export abstract class BaseToolComponent {
  private readonly activatedRoute = inject(ActivatedRoute);
  protected readonly userSettingsService = inject(UserSettingsService);

  protected toolCode: ToolCode;

  constructor() {
    this.activatedRoute.data
      .pipe(takeUntilDestroyed())
      .subscribe({
        next: (data) => {
          this.toolCode = data[RouteData.ToolCode];
        }
      });
  }

  protected getUserSettings<T = unknown>(model: UserSettingsModel): Observable<T> {
    return this.userSettingsService.getSettings<T>(model);
  }
}
