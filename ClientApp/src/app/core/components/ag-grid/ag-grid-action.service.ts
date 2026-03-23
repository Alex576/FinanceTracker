import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { UpdateGridModel } from './models/update-grid-model';

@Injectable({
  providedIn: 'root'
})
export class AgGridActionService {
  private readonly gridTransitionSub$ = new Subject<UpdateGridModel>();
  readonly gridTransition$ = this.gridTransitionSub$.asObservable();

  applyTransition(data: UpdateGridModel): void {
    this.gridTransitionSub$.next(data);
  }
}
