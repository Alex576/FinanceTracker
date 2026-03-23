import { inject, Injectable, Renderer2, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ColDef, colorSchemeDarkBlue, colorSchemeLight, GetRowIdParams, GridApi, GridOptions, ICellRendererParams, themeMaterial, ValueGetterParams } from "ag-grid-community";
import { ActionService } from '../../services/action.service';
import { AgGridActionService } from './ag-grid-action.service';
import { rendererActions } from './cell-renderers/actions-cell-renderer';
import { Column, PinPosition } from './models/column';
import { ColumnDataType } from './models/column-data-type';
import { Grid } from './models/grid';
import { Row } from './models/row';
import { UpdateGridModel } from './models/update-grid-model';

@Injectable()
export class AgGridService {
  private readonly renderer2 = inject(Renderer2);
  private readonly actionService = inject(ActionService);
  private readonly gridActionService = inject(AgGridActionService);

  gridApi: GridApi;

  readonly gridOptions = signal<GridOptions>(null);

  constructor() {
    this.gridActionService.gridTransition$
      .pipe(
        takeUntilDestroyed(),
      )
      .subscribe({
        next: ({ rowIndex, add, update, remove }: UpdateGridModel) => {
          const removedNodes = [];
          for (const rowId of remove) {
            const node = this.gridApi.getRowNode(rowId);
            if (node) {
              removedNodes.push(node.data);
            }
          }
          this.gridApi.applyTransaction({ addIndex: rowIndex, add, update, remove: removedNodes });
        }
      });
  }


  initializeGrid(grid: Grid): void {
    if (this.gridApi) {
      this.gridApi.updateGridOptions(this.getGridOptions(grid));
    }
    else {
      this.gridOptions.set(this.getInitialGridOptions(grid));
    }
  }

  private getGridOptions(grid: Grid): GridOptions {
    return {
      rowData: grid.rows,
      columnDefs: this.prepareCols(grid.layout.cols, grid.rows),
    };
  }

  private getInitialGridOptions(grid: Grid): GridOptions {
    return {
      theme: themeMaterial
        .withParams(Object.entries(colorSchemeLight.modeParams)[0][1], 'light-theme')
        .withParams(Object.entries(colorSchemeDarkBlue.modeParams)[0][1], 'dark-theme'),
      rowData: grid.rows,
      columnDefs: this.prepareCols(grid.layout.cols, grid.rows),
      getRowId: (params: GetRowIdParams<Row>) => params.data.tag.id,
    };
  }

  private prepareCols(cols: Column[], rows: Row[]): ColDef[] {
    const colDefs: ColDef[] = [];
    for (let i = 0; i < cols.length; i++) {
      const col = cols[i];
      const colDef: ColDef = {
        field: col.field,
        width: col.width,
        colId: col.columnId.toString(),
        editable: col.editable,
        filter: col.filter,
        pinned: this.getPin(col),
        lockPinned: col.lockPin,
        autoHeight: col.autoHeight,
        wrapText: col.wrapText,
        sortable: col.sortable,
        maxWidth: col.maxWidth,
        resizable: col.resizable,
        cellRendererParams: col,
        valueGetter: (params: ValueGetterParams<Row>) => {
          return params.data.data[+params.colDef.colId];
        },
        cellRendererSelector: (params: ICellRendererParams) => {
          const cellParams = params.colDef.cellRendererParams as Column;
          if (cellParams.columnDataType === ColumnDataType.Actions) {
            return {
              component: (params: ICellRendererParams) => rendererActions(params, rows[params.node.rowIndex], this.actionService),
            };
          }
          return undefined;
        }
      };

      colDefs.push(colDef);
    }

    return colDefs;
  }

  private getPin(column: Column): 'left' | 'right' | null {
    switch (column.pin) {
      case PinPosition.Left:
        return 'left';
      case PinPosition.Right:
        return 'right';
      default:
        return null;
    }
  }
}
