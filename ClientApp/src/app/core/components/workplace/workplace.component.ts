import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterOutlet } from "@angular/router";
import { forkJoin, tap } from 'rxjs';
import { MenuCodeIcon, MenuItem } from '../../models/menu-item';
import { UserSettingCode } from '../../models/user-setting-code';
import { NavigationService } from '../../services/navigation.service';
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
export class WorkplaceComponent extends BaseToolComponent implements OnInit {
  private readonly navigationMenuService = inject(NavigationMenuService);
  private readonly navigationService = inject(NavigationService);
  protected readonly items = signal<MenuItem[]>(null);
  protected readonly ready = signal<boolean>(false);

  constructor() {
    super();

    forkJoin([
      this.navigationMenuService.getMenuItems(),
      this.userSettingsService.getLastSessionSettings({ settingCode: UserSettingCode.LastOpenedTool })

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

  ngOnInit() {
  }

}
