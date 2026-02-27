import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { mergeMap, Observable, tap } from 'rxjs';
import { ChangedControlValue } from '../../models/controls/changed-control-value';
import { ComboControl } from '../../models/controls/combo-control';
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { FiltersComponent } from "../filters/filters.component";
import { LoadingComponent } from "../loading/loading.component";
import { FiltersEditorComponent } from "./filters-editor/filters-editor.component";
import { GridEditorComponent } from "./grid-editor/grid-editor.component";
import { LayoutEditorService } from './layout-editor.service';
import { LayoutEditorModel } from './models/layout-editor-model';
import { LayoutManagementModel } from './models/layout-management-model';
import { TileTypeCode } from './models/tile-type-code';

@Component({
  selector: 'app-layout-editor',
  templateUrl: './layout-editor.component.html',
  styleUrls: ['./layout-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [FiltersComponent, LoadingComponent, FiltersEditorComponent, GridEditorComponent],
  providers: [LayoutEditorService],
})
export class LayoutEditorComponent extends BaseToolComponent {
  private readonly service = inject(LayoutEditorService);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly itemType = TileTypeCode;

  protected readonly layoutManagement = signal<LayoutManagementModel>(null, { equal: () => false });
  protected readonly layoutEditor = signal<LayoutEditorModel>(null);

  protected readonly filters = computed<ComboControl[]>(() => {
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

  constructor() {
    super();

    this.service.getLayoutManagement()
      .pipe(
        tap({ next: (model) => this.layoutManagement.set(model) }),
        mergeMap((model) => this.loadLayoutEditor(model.toolFilter.value as number)),
        takeUntilDestroyed(),
      )
      .subscribe();
  }

  onFilterValueChanged({ control, newValue }: ChangedControlValue): void {
    if (control.id == 'ToolFilter') {
      this.loadLayoutEditor(newValue as number)
        .subscribe();
    }
  }


  private loadLayoutEditor(newValue: number): Observable<LayoutEditorModel> {
    return this.service.getLayoutEditor(newValue)
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
        takeUntilDestroyed(this.destroyRef)
      );
  }

  // protected getComponent(type: TileTypeCode): Type<FiltersComponent | AgGridComponent> {
  //   switch (type) {
  //     case TileTypeCode.Filter: return FiltersComponent;
  //     case TileTypeCode.Grid: return AgGridComponent;
  //     default: return null;
  //   }
  // }
}
