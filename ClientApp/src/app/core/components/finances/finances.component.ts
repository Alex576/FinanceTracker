import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { mergeMap, tap } from 'rxjs';
import { FormControl } from '../../models/controls/form-control';
import { getFormControlValues } from '../../models/controls/form-control-value';
import { AgGridComponent } from "../ag-grid/ag-grid.component";
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { DashboardPanelComponent } from "../dashboard-panel/dashboard-panel.component";
import { DashboardLayout } from '../dashboard-panel/models/dashboard-layout';
import { FiltersComponent } from "../filters/filters.component";
import { FinancesService } from './finances.service';

@Component({
  selector: 'app-finances',
  templateUrl: './finances.component.html',
  styleUrls: ['./finances.component.scss'],
  imports: [FiltersComponent, AgGridComponent, DashboardPanelComponent],
  providers: [FinancesService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FinancesComponent extends BaseToolComponent {
  private readonly service = inject(FinancesService);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly filters = signal<FormControl[]>(null);
  protected readonly dashboard = signal<DashboardLayout>(null);

  constructor() {
    super();

    this.service.getFilters(this.toolCode)
      .pipe(
        tap({ next: (filters) => this.filters.set(filters) }),
        mergeMap((filters) => this.service.getLayout({ toolCode: this.toolCode, filters: getFormControlValues(filters) })),
      )
      .subscribe({
        next: (dashboard: DashboardLayout) => {
          this.dashboard.set(dashboard);
        }
      });

  }

  onFilterChanged(control: FormControl): void {
    this.service.getLayout({ toolCode: this.toolCode, filters: getFormControlValues(this.filters()) })
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe({
        next: (layout) => {
          console.log(layout);
        }
      });
  }

}
