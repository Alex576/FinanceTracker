import { ChangeDetectionStrategy, Component, computed, inject, input, OnInit, output } from '@angular/core';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from "@angular/material/icon";
import { FormControl } from '../../../models/controls/form-control';
import { SidePanelType } from '../../../models/side-panel/side-panel-type';
import { TileCode } from '../../../models/tile-code';
import { SidePanelService } from '../../../services/side-panel.service';
import { FilterEditorComponent } from '../../side-panel/filter-editor/filter-editor.component';
import { LayoutEditorService } from '../layout-editor.service';
import { EditFilterModel } from '../models/edit-filter-model';

@Component({
  selector: 'app-filters-editor',
  templateUrl: './filters-editor.component.html',
  styleUrls: ['./filters-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatIconModule, MatIconButton, MatButtonModule]
})
export class FiltersEditorComponent implements OnInit {
  readonly filters = input.required<FormControl[]>();
  readonly tileCode = input.required<TileCode>();
  readonly maxItemCount = input<number>(Number.POSITIVE_INFINITY);

  readonly editItem = output<EditFilterModel>();

  private readonly sidePanelService = inject(SidePanelService);
  private readonly layoutEditorService = inject(LayoutEditorService);

  // protected readonly filters = computed<FormControl[]>(() => this.filters() ?? []);
  protected readonly canAdd = computed<boolean>(() => this.maxItemCount() > this.filters().length);

  constructor() { }

  ngOnInit() {
  }

  onEditItem(item: FormControl): void {
    this.editItem.emit({ control: item, tileCode: this.tileCode() });
  }

  onAddItem(): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutFilterEditor,
      componentType: FilterEditorComponent,
      data: { tileCode: this.tileCode(), data: {} }, header: 'Filter'
    },
      [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);
  }
}
