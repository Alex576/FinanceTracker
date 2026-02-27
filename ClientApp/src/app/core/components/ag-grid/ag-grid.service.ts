import { Injectable } from '@angular/core';
import { ColDef, GridApi, ValueGetterParams } from "ag-grid-community";
import { Column, PinPosition } from './models/column';

@Injectable()
export class AgGridService {

  gridApi: GridApi;

  constructor() { }
  prepareCols(cols: Column[]): ColDef[] {
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
        lockPinned: col.lockPinned,
        autoHeight: col.autoHeight,
        wrapText: col.wrapText,
        sortable: col.sortable,
        maxWidth: col.maxWidth,
        resizable: col.resizable,
        valueGetter: (params: ValueGetterParams) => {
          return params.data[+params.colDef.colId];
        }
      };

      colDefs.push(colDef);
    }

    return colDefs;
  }

  private getPin(column: Column): 'left' | 'right' | null {
    switch (column.pinned) {
      case PinPosition.Left:
        return 'left';
      case PinPosition.Right:
        return 'right';
      default:
        return null;
    }
  }
}
