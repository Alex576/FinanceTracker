import { ChangeDetectionStrategy, Component, computed, inject, input, OnInit } from '@angular/core';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from "@angular/material/icon";
import { FormControl } from '../../../models/controls/form-control';
import { SidePanelType } from '../../../models/side-panel/side-panel-type';
import { TileCode } from '../../../models/tile-code';
import { SidePanelService } from '../../../services/side-panel.service';
import { ItemEditorComponent } from '../../side-panel/layout-editors/item-editor/item-editor.component';
import { LayoutEditorService } from '../layout-editor.service';

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

  private readonly sidePanelService = inject(SidePanelService);
  private readonly layoutEditorService = inject(LayoutEditorService);

  // protected readonly filters = computed<FormControl[]>(() => this.filters() ?? []);
  protected readonly canAdd = computed<boolean>(() => this.maxItemCount() > this.filters().length);

  constructor() { }

  ngOnInit() {
  }

  onEditItem(item: FormControl): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutFilterEditor,
      componentType: ItemEditorComponent,
      tileCode: this.tileCode(),
      data: { itemId: item.id },
      header: 'Filter'
    },
      [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);
  }

  onAddItem(): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.LayoutFilterEditor,
      componentType: ItemEditorComponent,
      tileCode: this.tileCode(),
      data: {},
      header: 'Filter'
    },
      [{ provide: LayoutEditorService, useValue: this.layoutEditorService }]);
  }
}
