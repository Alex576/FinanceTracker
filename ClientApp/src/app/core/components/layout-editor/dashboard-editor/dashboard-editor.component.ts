import { ChangeDetectionStrategy, Component, computed, inject, input } from '@angular/core';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EditorType } from '../../../models/form-editor/editor-type';
import { SidePanelType } from '../../../models/side-panel/side-panel-type';
import { TileCode } from '../../../models/tile-code';
import { SidePanelService } from '../../../services/side-panel.service';
import { Constants } from '../../../utils/constants';
import { DashboardPanelComponent } from "../../dashboard-panel/dashboard-panel.component";
import { DashboardItem } from '../../dashboard-panel/models/dashboard-item';
import { DashboardLayout } from '../../dashboard-panel/models/dashboard-layout';
import { DashboardOptions } from '../../dashboard-panel/models/dashboard-options';
import { ItemEditorComponent } from '../../side-panel/layout-editors/item-editor/item-editor.component';
import { LayoutEditorService } from '../layout-editor.service';

@Component({
  selector: 'app-dashboard-editor',
  templateUrl: './dashboard-editor.component.html',
  styleUrls: ['./dashboard-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [DashboardPanelComponent, MatIconModule, MatIconButton, MatButtonModule]
})
export class DashboardEditorComponent {
  readonly dashboardLayout = input.required<DashboardLayout>();
  readonly tileCode = input.required<TileCode>();

  private readonly sidePanelService = inject(SidePanelService);
  private readonly layoutEditorService = inject(LayoutEditorService);

  constructor() { }
  protected readonly options = computed<DashboardOptions>(() => this.dashboardLayout().options);
  protected readonly items = computed<DashboardItem[]>(() => this.dashboardLayout().items);

  onAddItem(): void {
    this.onOpenItem(Constants.AddNewCode);
  }

  onOpenItem(id: string): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutDashboardItemEditor,
      componentType: ItemEditorComponent,
      tileCode: this.tileCode(),
      data: { itemId: id, editorType: EditorType.Dashboard },
      header: 'Dashboard Item'
    },
      [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);
  }
}
