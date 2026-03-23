import { Injectable } from '@angular/core';
import { merge, Observable, Subject } from 'rxjs';
import { RowAction } from '../components/ag-grid/models/row-action';
import { GridActionModel } from '../models/grid-action-model';

@Injectable({
  providedIn: 'root'
})
export class ActionService {
  private readonly actionsMap = new Map<RowAction, Subject<GridActionModel>>();

  constructor() {
    Object.values(RowAction).filter((action) => typeof action === 'number').forEach((action) => {
      this.actionsMap.set(action, new Subject());
    });
  }

  observe(...actions: RowAction[]): Observable<GridActionModel> {
    return merge(...actions.map((action) => this.actionsMap.get(action)));
  }


  setAction(model: GridActionModel): void {
    this.actionsMap.get(model.action).next(model);
  }
}
