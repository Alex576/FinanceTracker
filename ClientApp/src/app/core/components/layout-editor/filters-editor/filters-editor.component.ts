import { ChangeDetectionStrategy, Component, computed, inject, input, OnInit } from '@angular/core';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from "@angular/material/icon";
import { SidePanelType } from '../../../models/side-panel/side-panel-type';
import { TileCode } from '../../../models/tile-code';
import { SidePanelService } from '../../../services/side-panel.service';
import { FilterEditorComponent } from '../../side-panel/filter-editor/filter-editor.component';
import { FilterControlEntity, FilterLayoutEntity } from '../models/layout-editable-item';

@Component({
  selector: 'app-filters-editor',
  templateUrl: './filters-editor.component.html',
  styleUrls: ['./filters-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatIconModule, MatIconButton, MatButtonModule]
})
export class FiltersEditorComponent implements OnInit {
  readonly filterEntity = input.required<FilterLayoutEntity>();
  readonly tileCode = input.required<TileCode>();
  readonly maxItemCount = input<number>(Number.POSITIVE_INFINITY);

  private readonly sidePanelService = inject(SidePanelService);

  protected readonly filters = computed<FilterControlEntity[]>(() => this.filterEntity()?.filters ?? []);
  protected readonly canAdd = computed<boolean>(() => this.maxItemCount() > this.filters().length);

  constructor() { }

  ngOnInit() {
  }

  onEditItem(item: FilterControlEntity): void {
    console.log(item);
  }

  onAddItem(): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutFilterEditor,
      componentType: FilterEditorComponent,
      data: { tileCode: this.tileCode(), data: {} }, header: 'Filter'
    });
  }
}
