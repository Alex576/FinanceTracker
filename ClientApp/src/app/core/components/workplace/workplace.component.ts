import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterOutlet } from "@angular/router";
import { forkJoin, tap } from 'rxjs';
import { MenuCodeIcon, MenuItem } from '../../models/menu-item';
import { UserSettingCode } from '../../models/user-setting-code';
import { AppService } from '../../services/app.service';
import { NavigationService } from '../../services/navigation.service';
import { LanguageCode, TranslateService } from '../../services/translate.service';
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { LoadingComponent } from '../loading/loading.component';
import { NavigationMenuComponent } from "../navigation-menu/navigation-menu.component";
import { NavigationMenuService } from '../navigation-menu/navigation-menu.service';
import { ToolbarComponent } from "../toolbar/toolbar.component";

@Component({
  selector: 'app-workplace',
  templateUrl: './workplace.component.html',
  styleUrls: ['./workplace.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NavigationMenuComponent, RouterOutlet, ToolbarComponent, LoadingComponent],
  providers: [NavigationMenuService],
})
export class WorkplaceComponent extends BaseToolComponent {
  private readonly navigationMenuService = inject(NavigationMenuService);
  private readonly navigationService = inject(NavigationService);
  protected readonly items = signal<MenuItem[]>(null);
  protected readonly ready = signal<boolean>(false);
  private readonly service = inject(AppService);
  private readonly translateService = inject(TranslateService);

  constructor() {
    super();

    forkJoin([
      this.navigationMenuService.getMenuItems(),
      this.userSettingsService.getLastSessionSettings({ settingCode: UserSettingCode.LastOpenedTool }),
      this.service.getAppConfig(),
      this.translateService.loadTranslationsAsync(LanguageCode.EN),
    ])
      .pipe(
        tap({
          next: ([items, sessionSetting]) => {
            items.forEach((item) => {
              item.icon = MenuCodeIcon.get(item.id);
            });
            this.items.set(items);
            this.navigationService.validateUrl(sessionSetting);
          }
        }),
        takeUntilDestroyed()
      )
      .subscribe({ next: () => this.ready.set(true) });
  }
}
