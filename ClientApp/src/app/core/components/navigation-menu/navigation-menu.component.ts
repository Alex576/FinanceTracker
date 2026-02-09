import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { MatButtonModule, MatIconButton } from "@angular/material/button";
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NavigationItem } from '../../models/navigation/navigation-item';
import { ToolCodeUrlMap } from '../../models/tool-code';
import { NavigationService } from '../../services/navigation.service';
import { ThemeSwitcherService } from '../../services/theme-switcher.service';
import { NavigationCode } from '../../utils/navigation-codes';

@Component({
  selector: 'app-navigation-menu',
  templateUrl: './navigation-menu.component.html',
  styleUrls: ['./navigation-menu.component.scss'],
  imports: [MatSidenavModule, MatButtonModule, MatIconButton, MatIconModule],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavigationMenuComponent {
  private readonly themeSwitcherService = inject(ThemeSwitcherService);
  private readonly navigationService = inject(NavigationService);

  protected expanded = signal<boolean>(false);
  protected readonly elementAttribute = computed(() => this.expanded() ? 'no-border' : 'no-border hide-text');
  protected items: NavigationItem[] = [];

  constructor() {
    Object.entries(NavigationCode).forEach(([key, value]) => {
      if (isNaN(Number(key))) {
        this.items.push({ id: Number(value), title: key, icon: key.toLowerCase() });
      }
    });
  }

  onExpand(): void {
    this.expanded.set(true);
  }

  onCollapse(): void {
    this.expanded.set(false);
  }

  onElementClick(item: NavigationItem) {
    this.navigationService.workplaceNavigate(ToolCodeUrlMap.get(item.id) ?? '');
  }

  onToggleTheme(): void {
    this.themeSwitcherService.onToggleTheme();
  }
}
