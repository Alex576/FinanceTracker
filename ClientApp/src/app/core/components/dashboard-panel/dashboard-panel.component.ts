import { ChangeDetectionStrategy, Component, computed, effect, inject, input, output } from '@angular/core';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from "@angular/material/icon";
import { Gridster, GridsterItem } from 'angular-gridster2';
import { Constants } from '../../utils/constants';
import { DashboardPanelService } from './dashboard-panel.service';
import { DashboardItem } from './models/dashboard-item';
import { DashboardOptions } from './models/dashboard-options';

@Component({
  selector: 'app-dashboard-panel',
  templateUrl: './dashboard-panel.component.html',
  styleUrls: ['./dashboard-panel.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [Gridster, GridsterItem, MatIconModule, MatIconButton, MatButtonModule],
  providers: [DashboardPanelService]
})
export class DashboardPanelComponent {
  readonly options = input.required<DashboardOptions>();
  readonly items = input.required<DashboardItem[]>();

  readonly openItem = output<string>();

  private readonly service = inject(DashboardPanelService);

  protected readonly addNewItemCode = Constants.AddNewCode;
  protected readonly gridsterConfig = computed(() => this.service.gridsterConfig());
  protected readonly dashboardItems$ = computed(() => this.service.items());
  constructor() {
    effect(() => this.service.initialize(this.options()));
    effect(() => this.service.initItems(this.items()));
  }

  onOpenItem(id: string): void {
    this.openItem.emit(id);
  }
}
