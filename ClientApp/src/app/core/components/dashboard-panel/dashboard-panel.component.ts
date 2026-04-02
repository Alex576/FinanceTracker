import { ChangeDetectionStrategy, Component, effect, inject, input } from '@angular/core';
import { Gridster, GridsterItem } from 'angular-gridster2';
import { DashboardPanelService } from './dashboard-panel.service';
import { DashboardItem } from './models/dashboard-item';
import { DashboardOptions } from './models/dashboard-options';

@Component({
  selector: 'app-dashboard-panel',
  templateUrl: './dashboard-panel.component.html',
  styleUrls: ['./dashboard-panel.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [Gridster, GridsterItem],
  providers: [DashboardPanelService]
})
export class DashboardPanelComponent {
  readonly options = input.required<DashboardOptions>();
  readonly items = input.required<DashboardItem[]>();
  private readonly service = inject(DashboardPanelService);

  constructor() {
    effect(() => {
      this.service.initialize(this.options());
    });
  }


}
