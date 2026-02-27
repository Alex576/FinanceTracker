import { ChangeDetectionStrategy, Component, inject, input } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { SIDE_PANEL_HEADER } from '../../../models/side-panel/side-panel-data-token';
import { SidePanelService } from '../../../services/side-panel.service';
import { LoadingComponent } from "../../loading/loading.component";

@Component({
  selector: 'app-side-panel-view',
  templateUrl: './side-panel-view.component.html',
  styleUrls: ['./side-panel-view.component.scss'],
  imports: [MatButton, LoadingComponent],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SidePanelViewComponent {
  readonly isMainDataReady = input<boolean>(true);

  protected readonly header = inject(SIDE_PANEL_HEADER);
  private readonly sidePanelService = inject(SidePanelService);

  onClose(): void {
    this.sidePanelService.close();
  }
}
