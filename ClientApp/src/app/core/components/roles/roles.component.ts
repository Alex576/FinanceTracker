import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AgGridComponent } from "../ag-grid/ag-grid.component";
import { Grid } from '../ag-grid/models/grid';
import { LoadingComponent } from "../loading/loading.component";
import { RolesService } from './roles.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AgGridComponent, LoadingComponent],
  providers: [RolesService],
})
export class RolesComponent implements OnInit {
  private readonly service = inject(RolesService);

  protected readonly gridLayout = signal<Grid>(null);

  constructor() {
    this.service.getRolesGrid()
      .pipe(
        takeUntilDestroyed()
      )
      .subscribe({ next: (layout) => this.gridLayout.set(layout) });
  }

  ngOnInit() {
  }

}
