import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IHeaderAngularComp } from 'ag-grid-angular';
import { IHeaderParams } from 'ag-grid-community';

@Component({
  selector: 'app-action-column',
  templateUrl: './action-column.component.html',
  styleUrls: ['./action-column.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ActionColumnComponent implements IHeaderAngularComp {

  agInit(params: IHeaderParams<any, any>): void {
    throw new Error('Method not implemented.');
  }
  refresh(params: IHeaderParams): boolean {
    throw new Error('Method not implemented.');
  }
}
