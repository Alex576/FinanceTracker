import { ChangeDetectionStrategy, Component, computed, inject, input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { GridActionModel } from '../../../models/grid-action-model';
import { SidePanelType } from '../../../models/side-panel/side-panel-type';
import { TileCode } from '../../../models/tile-code';
import { ActionService } from '../../../services/action.service';
import { SidePanelService } from '../../../services/side-panel.service';
import { AgGridComponent } from "../../ag-grid/ag-grid.component";
import { Grid } from '../../ag-grid/models/grid';
import { RowAction } from '../../ag-grid/models/row-action';
import { ItemEditorComponent } from '../../side-panel/layout-editors/item-editor/item-editor.component';
import { LayoutEditorService } from '../layout-editor.service';
import { GridLayoutEntity } from '../models/layout-editable-item';

@Component({
  selector: 'app-grid-editor',
  templateUrl: './grid-editor.component.html',
  styleUrls: ['./grid-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatIconModule, MatIconButton, MatButtonModule, AgGridComponent],
})
export class GridEditorComponent {
  readonly gridEntity = input.required<GridLayoutEntity>();
  readonly tileCode = input.required<TileCode>();

  private readonly sidePanelService = inject(SidePanelService);
  private readonly layoutEditorService = inject(LayoutEditorService);
  private readonly actionService = inject(ActionService);
  private readonly layoutService = inject(LayoutEditorService);

  protected readonly grid = computed<Grid>(() => this.gridEntity()?.gridEditor?.gridEntity);
  constructor() {
    this.actionService.observe(RowAction.Edit)
      .pipe(
        takeUntilDestroyed(),
      )
      .subscribe({
        next: ({ data }: GridActionModel) => {
          this.sidePanelService.openSidePanel({
            type: SidePanelType.LayoutGridColumnEditor,
            componentType: ItemEditorComponent,
            tileCode: this.tileCode(),
            data: data,
            header: 'Column'
          },
            [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);
        }
      });

    this.actionService.observe(RowAction.Remove)
      .pipe(
        takeUntilDestroyed(),
      )
      .subscribe({
        next: ({ data }: GridActionModel) => {
          this.layoutService.removeLayoutItemAsync({ tileCode: this.tileCode(), itemId: data.id });
        }
      });
  }

  onAddColumn(): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutGridColumnEditor,
      componentType: ItemEditorComponent,
      tileCode: this.tileCode(),
      data: {},
      header: 'Column'
    },
      [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);
  }
}
