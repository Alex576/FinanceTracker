import { ChangeDetectionStrategy, Component, computed, inject, input, OnInit } from '@angular/core';
import { AgGridAngular } from "ag-grid-angular";
import { colorSchemeDarkBlue, colorSchemeLight, GridOptions, GridReadyEvent, themeMaterial } from 'ag-grid-community';
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

  protected readonly gridOptions = computed<GridOptions>(() => {
    const grid = this.grid();
    return {
      theme: themeMaterial
        .withParams(Object.entries(colorSchemeLight.modeParams)[0][1], 'light-theme')
        .withParams(Object.entries(colorSchemeDarkBlue.modeParams)[0][1], 'dark-theme'),
      rowData: grid.rows,
      columnDefs: this.service.prepareCols(grid.layout.cols),
    };
  });

  protected readonly ready = computed<boolean>(() => !!this.gridOptions());

  constructor() {

  }

  ngOnInit() {
  }


  onGridReady(params: GridReadyEvent): void {
    this.service.gridApi = params.api;
  }
}
