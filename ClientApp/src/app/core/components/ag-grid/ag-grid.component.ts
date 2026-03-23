import { ChangeDetectionStrategy, Component, computed, effect, inject, input, OnInit } from '@angular/core';
import { AgGridAngular } from "ag-grid-angular";
import { GridOptions, GridReadyEvent } from 'ag-grid-community';
import { LoadingComponent } from "../loading/loading.component";
import { AgGridService } from './ag-grid.service';
import { Grid } from './models/grid';

@Component({
  selector: 'app-ag-grid',
  templateUrl: './ag-grid.component.html',
  styleUrls: ['./ag-grid.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AgGridAngular, LoadingComponent],
  providers: [AgGridService],
})
export class AgGridComponent implements OnInit {
  readonly grid = input.required<Grid>();

  private readonly service = inject(AgGridService);

  protected readonly gridOptions = computed<GridOptions>(() => this.service.gridOptions());

  protected readonly ready = computed<boolean>(() => !!this.gridOptions());

  constructor() {

    effect(() => {
      const grid = this.grid();
      if (!grid) { return; }

      this.service.initializeGrid(grid);
    });
  }

  ngOnInit() {
  }


  onGridReady(params: GridReadyEvent): void {
    this.service.gridApi = params.api;
  }
}
