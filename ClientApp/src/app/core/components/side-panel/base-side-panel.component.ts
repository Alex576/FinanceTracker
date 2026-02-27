import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { FormModel } from '../../models/form-editor/form-model';
import { SIDE_PANEL_DATA } from '../../models/side-panel/side-panel-data-token';

@Component({
    selector: 'app-base-side-panel',
    template: ``,
    styleUrls: [],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export abstract class BaseSidePanelComponent {
    protected readonly data = inject(SIDE_PANEL_DATA);
    protected readonly form = signal<FormModel>(null);

    protected readonly isFormReady = computed<boolean>(() => !!this.form());
}
