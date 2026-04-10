import { Injectable, signal, untracked } from '@angular/core';
import { CompactType, DisplayGrid, Gridster, GridsterApi, GridsterConfig, GridType } from 'angular-gridster2';
import { DashboardItem } from './models/dashboard-item';
import { DashboardOptions } from './models/dashboard-options';

@Injectable()
export class DashboardPanelService {
  private readonly MIN_ITEM_SIZE = 4;
  readonly gridsterConfig = signal<GridsterConfig>(null);

  readonly items = signal<DashboardItem[]>([]);

  private api: GridsterApi;
  private gridster: Gridster;

  initialize(options: DashboardOptions): void {
    const config = this.getConfig();
    this.gridsterConfig.set(config);
  }

  private getConfig(): GridsterConfig {
    return {
      initCallback: (gridster: Gridster, api: GridsterApi) => {
        this.gridster = gridster;
        this.api = api;
      },
      gridType: GridType.ScrollVertical,
      compactType: CompactType.None,
      margin: 10,
      outerMargin: true,
      outerMarginTop: null,
      outerMarginRight: null,
      outerMarginBottom: null,
      outerMarginLeft: null,
      useTransformPositioning: true,
      mobileBreakpoint: 640,
      useBodyForBreakpoint: false,
      minCols: 30,
      maxCols: 100,
      minRows: 10,
      maxRows: 100,
      maxItemCols: 100,
      minItemCols: this.MIN_ITEM_SIZE,
      maxItemRows: 100,
      minItemRows: this.MIN_ITEM_SIZE,
      maxItemArea: 2500,
      minItemArea: 1,
      defaultItemCols: this.MIN_ITEM_SIZE,
      defaultItemRows: this.MIN_ITEM_SIZE,
      // fixedColWidth: 105,
      // fixedRowHeight: 105,
      keepFixedHeightInMobile: false,
      keepFixedWidthInMobile: false,
      scrollSensitivity: 10,
      scrollSpeed: 20,
      enableEmptyCellClick: false,
      enableEmptyCellContextMenu: false,
      enableEmptyCellDrop: false,
      enableEmptyCellDrag: false,
      enableOccupiedCellDrop: false,
      emptyCellDragMaxCols: 50,
      emptyCellDragMaxRows: 50,
      ignoreMarginInRow: false,
      draggable: {
        enabled: true,
        ignoreContent: true,
        dragHandleClass: 'item__header',
        ignoreContentClass: 'item__header__actions',
      },
      resizable: {
        enabled: true
      },
      swap: false,
      pushItems: true,
      disablePushOnDrag: false,
      disablePushOnResize: false,
      pushDirections: { north: true, east: true, south: true, west: true },
      pushResizeItems: false,
      displayGrid: DisplayGrid.Always,
      disableWindowResize: false,
      disableWarnings: false,
      scrollToNewItems: false,
    };
  }

  initItems(items: DashboardItem[]): void {
    const dashboardItems: DashboardItem[] = [];
    const oldItems = untracked(() => this.items());
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      item.rows ||= this.MIN_ITEM_SIZE;
      item.cols ||= this.MIN_ITEM_SIZE;
      if (this.gridster) {
        const oldItem = oldItems.find(x => x.id === item.id);
        if (oldItem) {
          item.x = oldItem.x;
          item.y = oldItem.y;
        } else {
          const { x, y } = this.gridster.getFirstPossiblePosition(item);
          item.x = x;
          item.y = y;
        }
      } else {
        item.x ||= -1;
        item.y ||= -1;
      }
      dashboardItems.push(item);
    }
    this.items.set(dashboardItems);
  }
}
