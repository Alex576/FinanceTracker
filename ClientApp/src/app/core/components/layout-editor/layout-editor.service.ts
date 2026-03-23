import { computed, DestroyRef, inject, Injectable, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { mergeMap, Observable, tap } from 'rxjs';
import { ComboControl } from '../../models/controls/combo-control';
import { RemoveItemModel } from '../../models/form-editor/delete-control-model';
import { OperationResult } from '../../models/operation-result/operation-result';
import { isSuccess } from '../../models/operation-result/result-code';
import { SidePanelType } from '../../models/side-panel/side-panel-type';
import { TileCode } from '../../models/tile-code';
import { ToolCode } from '../../models/tool-code';
import { SidePanelService } from '../../services/side-panel.service';
import { AgGridActionService } from '../ag-grid/ag-grid-action.service';
import { UpdateGridModel } from '../ag-grid/models/update-grid-model';
import { FilterEditorComponent } from '../side-panel/filter-editor/filter-editor.component';
import { LayoutEditorApiService } from './layout-editor-api.service';
import { EditFilterModel } from './models/edit-filter-model';
import { LayoutEditorModel } from './models/layout-editor-model';
import { LayoutManagementModel } from './models/layout-management-model';
import { TileTypeCode } from './models/tile-type-code';

@Injectable()
export class LayoutEditorService {
  private readonly sidePanelService = inject(SidePanelService);
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

  editFilter({ tileCode, control }: EditFilterModel) {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutFilterEditor,
      componentType: FilterEditorComponent,
      data: { tileCode: tileCode, data: control }, header: 'Filter'
    },
      [{ provide: LayoutEditorService, useValue: this }]);
  }

  applyEditorLayout(result: LayoutEditorModel): void {
    this.layoutEditor.set(result);
  }

  removeLayoutItemAsync(model: RemoveItemModel): void {
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
        if (item.data.type === TileTypeCode.Filter) {
          item.data.filters = item.data.filters.filter((filter) => filter.id !== elementId);
        }
        else if (item.data.type === TileTypeCode.Grid) {
          this.gridService.applyTransition(new UpdateGridModel([], [], [elementId]));
        }
      }
      return layout;
    });
  }
}
