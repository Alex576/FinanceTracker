import { computed, DestroyRef, inject, Injectable, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { mergeMap, Observable, tap } from 'rxjs';
import { ComboControl } from '../../models/controls/combo-control';
import { FullScreenPanelType } from '../../models/full-screen-panel/full-screen-panel-type';
import { OperationResult } from '../../models/operation-result/operation-result';
import { isSuccess } from '../../models/operation-result/result-code';
import { TileCode } from '../../models/tile-code';
import { ToolCode } from '../../models/tool-code';
import { FullScreenPanelService } from '../../services/full-screen-panel.service';
import { SidePanelService } from '../../services/side-panel.service';
import { AgGridActionService } from '../ag-grid/ag-grid-action.service';
import { UpdateGridModel } from '../ag-grid/models/update-grid-model';
import { FormEditorComponent } from '../full-screen-panel/form-editor/form-editor.component';
import { RemoveLayoutItemModel } from '../side-panel/layout-editors/item-editor/remove-layout-item-model';
import { LayoutEditorApiService } from './layout-editor-api.service';
import { FormLayoutEntity } from './models/layout-editable-item';
import { LayoutEditorModel } from './models/layout-editor-model';
import { LayoutManagementModel } from './models/layout-management-model';
import { TileTypeCode } from './models/tile-type-code';

@Injectable()
export class LayoutEditorService {
  private readonly sidePanelService = inject(SidePanelService);
  private readonly fullScreenPanelService = inject(FullScreenPanelService);
  private readonly api = inject(LayoutEditorApiService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly gridService = inject(AgGridActionService);

  readonly layoutManagement = signal<LayoutManagementModel>(null, { equal: () => false });
  readonly layoutEditor = signal<LayoutEditorModel>(null, { equal: () => false });

  readonly filters = computed<ComboControl[]>(() => {
    const filters: ComboControl[] = [];
    const model = this.layoutManagement();
    if (!model) { return filters; }

    if (model.toolFilter) {
      filters.push(model.toolFilter);
    }
    if (model.tileFilter) {
      filters.push(model.tileFilter);
    }
    return filters;
  });

  loadLayoutManagement(): void {
    this.api.getLayoutManagement()
      .pipe(
        tap({ next: (model) => this.layoutManagement.set(model) }),
        mergeMap((model) => this.loadLayoutEditor(model.toolFilter.value as number)),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  loadLayoutEditorAsync(newValue: number): void {
    this.loadLayoutEditor(newValue)
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  private loadLayoutEditor(newValue: number): Observable<LayoutEditorModel> {
    return this.getLayoutEditor(newValue)
      .pipe(
        tap({
          next: (model: LayoutEditorModel) => {
            this.layoutEditor.set(model);
            return this.layoutManagement.update(x => {
              x.tileFilter = model.tileFilter;
              return x;
            });
          }
        }),
      );
  }

  getLayoutEditor(toolCode: ToolCode): Observable<LayoutEditorModel> {
    return this.api.getLayoutEditor(toolCode);
  }

  applyEditorLayout(result: LayoutEditorModel): void {
    this.layoutEditor.set(result);
  }

  removeLayoutItemAsync(model: RemoveLayoutItemModel): void {
    this.api.removeLayoutItem(model)
      .pipe(
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: (result: OperationResult) => {
          if (isSuccess(result.code)) {
            this.removeLayoutElement(model.tileCode, model.itemId);
          }
        }
      });
  }

  removeLayoutElement(tileCode: TileCode, elementId: string): void {
    this.layoutEditor.update((layout) => {
      const item = layout.layoutItems.find(x => x.tileCode == tileCode);
      if (item) {
        switch (item.data.type) {
          case TileTypeCode.Filter:
            item.data.filters = item.data.filters.filter((filter) => filter.id !== elementId);
            break;
          case TileTypeCode.Grid:
            this.gridService.applyTransition(new UpdateGridModel([], [], [elementId]));
            break;
          case TileTypeCode.Dashboard:
            item.data.dashboardLayout = { ...item.data.dashboardLayout, items: item.data.dashboardLayout.items.filter(x => x.id !== elementId) };
            break;
          default:
            console.error('Not implemented');
            break;
        }
      }
      return layout;
    });
  }

  editForm(tileCode: TileCode, data: FormLayoutEntity): void {
    this.fullScreenPanelService.openFullScreenPanel({
      type: FullScreenPanelType.LayoutFormEditor,
      componentType: FormEditorComponent,
      tileCode,
      data,
      header: 'Form editor'
    },
      [{ provide: LayoutEditorService, useValue: this }]);
  }
}
