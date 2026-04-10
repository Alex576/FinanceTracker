import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { ComboControl } from '../../models/controls/combo-control';
import { FormControl } from '../../models/controls/form-control';
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { FiltersComponent } from "../filters/filters.component";
import { LoadingComponent } from "../loading/loading.component";
import { DashboardEditorComponent } from "./dashboard-editor/dashboard-editor.component";
import { FiltersEditorComponent } from "./filters-editor/filters-editor.component";
import { GridEditorComponent } from "./grid-editor/grid-editor.component";
import { LayoutEditorService } from './layout-editor.service';
import { TileTypeCode } from './models/tile-type-code';

@Component({
  selector: 'app-layout-editor',
  templateUrl: './layout-editor.component.html',
  styleUrls: ['./layout-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [FiltersComponent, LoadingComponent, FiltersEditorComponent, GridEditorComponent, DashboardEditorComponent],
  providers: [LayoutEditorService],
})
export class LayoutEditorComponent extends BaseToolComponent {
  private readonly service = inject(LayoutEditorService);

  protected readonly itemType = TileTypeCode;

  protected readonly layoutManagement = this.service.layoutManagement;
  protected readonly layoutEditor = this.service.layoutEditor;
  protected readonly filters = computed<ComboControl[]>(() => this.service.filters());

  constructor() {
    super();

    this.service.loadLayoutManagement();
  }

  onFilterValueChanged(control: FormControl): void {
    if (control.id == 'ToolFilter') {
      this.service.loadLayoutEditorAsync(control.value as number);
    }
  }
}
