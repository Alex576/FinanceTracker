import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';
import { RouteData } from '../../models/route-data';
import { ToolCode } from '../../models/tool-code';

@Component({
  selector: 'app-base-tool',
  template: ``,
  styleUrls: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export abstract class BaseToolComponent {
  private readonly activatedRoute = inject(ActivatedRoute);
  protected toolCode: ToolCode;
  constructor() {
    this.activatedRoute.data
      .pipe(takeUntilDestroyed())
      .subscribe({
        next: (data) => {
          this.toolCode = data[RouteData.ToolCode];
        }
      });
  }
}
