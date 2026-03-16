import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AgGridAngular } from 'ag-grid-angular';
import { colorSchemeDarkBlue, colorSchemeLight, themeMaterial, type ColDef, type GridOptions } from 'ag-grid-community';
import { mergeMap, tap } from 'rxjs';
import { FormControl } from '../../models/controls/form-control';
import { getFormControlValues } from '../../models/controls/form-control-value';
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { FiltersComponent } from "../filters/filters.component";
import { FinancesService } from './finances.service';

@Component({
  selector: 'app-finances',
  templateUrl: './finances.component.html',
  styleUrls: ['./finances.component.scss'],
  imports: [AgGridAngular, FiltersComponent],
  providers: [FinancesService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FinancesComponent extends BaseToolComponent {
  private readonly service = inject(FinancesService);
  private readonly destroyRef = inject(DestroyRef);

  // private readonly destroyRef = inject(DestroyRef);
  protected readonly rowData: any[] = [];
  protected readonly colDefs: ColDef[] = [
    { field: "make" },
    { field: "model" },
    { field: "price" },
    { field: "electric" }
  ];

  protected gridOptions: GridOptions;
  protected filters = signal<FormControl[]>(null);

  constructor() {
    super();
    this.gridOptions = {
      theme: themeMaterial
        .withParams(Object.entries(colorSchemeLight.modeParams)[0][1], 'light-theme')
        .withParams(Object.entries(colorSchemeDarkBlue.modeParams)[0][1], 'dark-theme'),
    };


    this.service.getFilters(this.toolCode)
      .pipe(
        tap({ next: (filters) => this.filters.set(filters) }),
        mergeMap((filters) => this.service.getLayout({ toolCode: this.toolCode, filters: getFormControlValues(filters) })),
      )
      .subscribe({
        next: (layout) => {
          console.log(layout);
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
