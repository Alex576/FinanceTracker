import { ChangeDetectionStrategy, Component, inject, input } from '@angular/core';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SidePanelType } from '../../../models/side-panel/side-panel-type';
import { TileCode } from '../../../models/tile-code';
import { SidePanelService } from '../../../services/side-panel.service';
import { GridColumnEditorComponent } from '../../side-panel/grid-column-editor/grid-column-editor.component';
import { LayoutEditorService } from '../layout-editor.service';
import { GridLayoutEntity } from '../models/layout-editable-item';

@Component({
  selector: 'app-grid-editor',
  templateUrl: './grid-editor.component.html',
  styleUrls: ['./grid-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatIconModule, MatIconButton, MatButtonModule],
})
export class GridEditorComponent {
  readonly gridEntity = input.required<GridLayoutEntity>();
  readonly tileCode = input.required<TileCode>();

  private readonly sidePanelService = inject(SidePanelService);
  private readonly layoutEditorService = inject(LayoutEditorService);

  constructor() { }

  onAddColumn(): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutGridColumnEditor,
      componentType: GridColumnEditorComponent,
      data: { tileCode: this.tileCode(), data: {} }, header: 'Column'
    },
      [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);

  }

}
