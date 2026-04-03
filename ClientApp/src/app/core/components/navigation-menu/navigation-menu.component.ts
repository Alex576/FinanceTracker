import { ChangeDetectionStrategy, Component, computed, effect, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule, MatIconButton } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { LocalStorageKeys } from '../../models/local-storage-keys';
import { MenuCode, MenuItem, ToolCodeUrlMap } from '../../models/menu-item';
import { TreeFlatItem } from '../../models/tree-flat-item-model';
import { UserSettingCode } from '../../models/user-setting-code';
import { LastSessionSetting } from '../../models/user-settings/last-session-setting';
import { NavigationService } from '../../services/navigation.service';
import { StorageService } from '../../services/storage.service';
import { ThemeSwitcherService } from '../../services/theme-switcher.service';
import { UserSettingsService } from '../../services/user-settings.service';
import { TreeFlatViewComponent } from "../tree-flat-view/tree-flat-view.component";
import { NavigationMenuService } from './navigation-menu.service';

@Component({
  selector: 'app-navigation-menu',
  templateUrl: './navigation-menu.component.html',
  styleUrls: ['./navigation-menu.component.scss'],
  imports: [MatSidenavModule, MatButtonModule, MatIconButton, MatIconModule, MatFormFieldModule, MatSlideToggleModule, ReactiveFormsModule, MatTooltipModule],
  providers: [NavigationMenuService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavigationMenuComponent extends TreeFlatViewComponent<MenuItem> {
  private readonly themeSwitcherService = inject(ThemeSwitcherService);
  private readonly navigationService = inject(NavigationService);
  private readonly userSettingsService = inject(UserSettingsService);
  private readonly storageService = inject(StorageService);

  protected expanded = signal<boolean>(false);
  protected readonly elementAttribute = computed(() => this.expanded() ? 'no-border' : 'no-border hide-text');

  protected readonly themeFormControl = new FormControl<boolean>(null);

  constructor() {
    super();

    this.initAutoThemeSwitch();

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

  private initAutoThemeSwitch(): void {
    this.themeFormControl.setValue(this.storageService.getValue<boolean>(LocalStorageKeys.AutoThemeSwitch) ?? false, { emitEvent: false });
    this.themeFormControl.valueChanges
      .pipe(takeUntilDestroyed())
      .subscribe({ next: (value) => this.themeSwitcherService.setAutoSwitchTheme(value) });
  }

  onExpand(): void {
    this.expanded.set(true);
  }

  onCollapse(): void {
    this.expanded.set(false);
  }

  onElementClick(item: TreeFlatItem<MenuItem>): void {
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
