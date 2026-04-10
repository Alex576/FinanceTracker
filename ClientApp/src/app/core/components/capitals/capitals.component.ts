import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { mergeMap, tap } from 'rxjs';
import { FormControl } from '../../models/controls/form-control';
import { getFormControlValues } from '../../models/controls/form-control-value';
import { SidePanelType } from '../../models/side-panel/side-panel-type';
import { TileCode } from '../../models/tile-code';
import { SidePanelService } from '../../services/side-panel.service';
import { BaseToolComponent } from '../base-tool/base-tool.component';
import { DashboardPanelComponent } from "../dashboard-panel/dashboard-panel.component";
import { DashboardLayout } from '../dashboard-panel/models/dashboard-layout';
import { FiltersComponent } from "../filters/filters.component";
import { CapitalEditorComponent } from '../side-panel/capital-editor/capital-editor.component';
import { CapitalsService } from './capitals.service';

@Component({
  selector: 'app-capitals',
  templateUrl: './capitals.component.html',
  styleUrls: ['./capitals.component.scss'],
  imports: [FiltersComponent, MatIconModule, MatIconButton, MatButtonModule, DashboardPanelComponent],
  providers: [CapitalsService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CapitalsComponent extends BaseToolComponent {
  private readonly service = inject(CapitalsService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly sidePanelService = inject(SidePanelService);

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

  onAddItem(): void {
    this.sidePanelService.openSidePanel({
      type: SidePanelType.CapitalEditor,
      componentType: CapitalEditorComponent,
      tileCode: TileCode.CapitalEditor,
      data: {},
      header: 'Edit Capital'
    });
  }

}
