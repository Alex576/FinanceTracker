import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AgGridAngular } from 'ag-grid-angular';
import { colorSchemeDarkBlue, colorSchemeLight, themeMaterial, type ColDef, type GridOptions } from 'ag-grid-community';
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { FinancesService } from './finances.service';

@Component({
  selector: 'app-finances',
  templateUrl: './finances.component.html',
  styleUrls: ['./finances.component.scss'],
  imports: [AgGridAngular],
  providers: [FinancesService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FinancesComponent extends BaseToolComponent implements OnInit {
  private readonly service = inject(FinancesService);
  private readonly destroyRef = inject(DestroyRef);
  protected readonly rowData: any[] = [];
  protected readonly colDefs: ColDef[] = [
    { field: "make" },
    { field: "model" },
    { field: "price" },
    { field: "electric" }
  ];

  protected gridOptions: GridOptions;
  constructor() {
    super();
    this.gridOptions = {
      theme: themeMaterial
        .withParams(Object.entries(colorSchemeLight.modeParams)[0][1], 'light-theme')
        .withParams(Object.entries(colorSchemeDarkBlue.modeParams)[0][1], 'dark-theme'),
    };
  }

  ngOnInit() {
    this.service.getLayout(this.toolCode)
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      ).subscribe({
        next: (layout) => {
          console.log(layout);
        }
      });

  }
  test() {
    this.service.getLayout(this.toolCode)
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      ).subscribe({
        next: (layout) => {
          console.log(layout);
        }
      });
  }
}
