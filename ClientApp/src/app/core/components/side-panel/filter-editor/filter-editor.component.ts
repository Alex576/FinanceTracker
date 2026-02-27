import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButton } from '@angular/material/button';
import { tap } from 'rxjs';
import { FormComponent } from "../../form/form.component";
import { BaseSidePanelComponent } from '../base-side-panel.component';
import { SidePanelViewComponent } from "../side-panel-view/side-panel-view.component";
import { FiltersEditorService } from './filters-editor.service';

@Component({
  selector: 'app-filter-editor',
  templateUrl: './filter-editor.component.html',
  styleUrls: ['./filter-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [SidePanelViewComponent, MatButton, FormComponent],
  providers: [FiltersEditorService],
})
export class FilterEditorComponent extends BaseSidePanelComponent {
  private readonly service = inject(FiltersEditorService);

  constructor() {
    super();

    this.service.getForm({ tileCode: this.data.tileCode })
      .pipe(
        tap({ next: (form) => this.form.set(form) }),
        takeUntilDestroyed()
      )
      .subscribe();
  }

  onSave(): void {

  }
}
