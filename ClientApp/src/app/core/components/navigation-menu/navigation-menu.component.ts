import { ChangeDetectionStrategy, Component, computed, effect, inject, signal } from '@angular/core';
import { MatButtonModule, MatIconButton } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MenuCode, MenuItem, ToolCodeUrlMap } from '../../models/menu-item';
import { TreeFlatItem } from '../../models/tree-flat-item-model';
import { UserSettingCode } from '../../models/user-setting-code';
import { LastSessionSetting } from '../../models/user-settings/last-session-setting';
import { NavigationService } from '../../services/navigation.service';
import { ThemeSwitcherService } from '../../services/theme-switcher.service';
import { UserSettingsService } from '../../services/user-settings.service';
import { TreeFlatViewComponent } from "../tree-flat-view/tree-flat-view.component";
import { NavigationMenuService } from './navigation-menu.service';

@Component({
  selector: 'app-navigation-menu',
  templateUrl: './navigation-menu.component.html',
  styleUrls: ['./navigation-menu.component.scss'],
  imports: [MatSidenavModule, MatButtonModule, MatIconButton, MatIconModule, MatFormFieldModule],
  providers: [NavigationMenuService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavigationMenuComponent extends TreeFlatViewComponent<MenuItem> {
  private readonly themeSwitcherService = inject(ThemeSwitcherService);
  private readonly navigationService = inject(NavigationService);
  private readonly userSettingsService = inject(UserSettingsService);

  protected expanded = signal<boolean>(false);
  protected readonly elementAttribute = computed(() => this.expanded() ? 'no-border' : 'no-border hide-text');

  constructor() {
    super();
    const settings = this.userSettingsService.getLoadedSettings<LastSessionSetting>({ settingCode: UserSettingCode.LastOpenedTool });
    this.selectedItem.set(settings?.lastOpenedTool || MenuCode.Dashboard);

    effect(() => {
      const selectedItem = this.selectedItem();
      if (selectedItem != settings.lastOpenedTool) {
        settings.lastOpenedTool = selectedItem;
        this.userSettingsService.saveUserLastSessionSettingAsync({ settingCode: UserSettingCode.LastOpenedTool }, settings);
      }
    });
  }

  onExpand(): void {
    this.expanded.set(true);
  }

  onCollapse(): void {
    this.expanded.set(false);
  }

  onElementClick(item: TreeFlatItem<MenuItem>) {
    if (item.hasChild) {
      this.expand(item);
    } else {
      this.navigationService.navigate(ToolCodeUrlMap.get(item.data.toolCode) ?? '');
      this.selectedItem.set(item.data.id);
    }
  }

  isActive(item: TreeFlatItem<MenuItem>): boolean {
    return item.data.id === this.selectedItem();
  }

  onToggleTheme(): void {
    this.themeSwitcherService.onToggleTheme();
  }
}
