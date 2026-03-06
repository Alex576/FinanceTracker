import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { FormModel } from '../../models/form-editor/form-model';
import { FormUpdateModel } from '../../models/form-editor/form-update-model';
import { SIDE_PANEL_DATA } from '../../models/side-panel/side-panel-data-token';
import { SidePanelService } from '../../services/side-panel.service';

@Component({
    selector: 'app-base-side-panel',
    template: ``,
    styleUrls: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export abstract class BaseSidePanelComponent {
    protected readonly panelData = inject(SIDE_PANEL_DATA);
    protected readonly sidePanelService = inject(SidePanelService);

    protected readonly form = signal<FormModel>(null);

    protected readonly isFormReady = computed<boolean>(() => !!this.form());

    protected getFormUpdateModel(itemId: string): FormUpdateModel {
        const form = this.form();
        const model: FormUpdateModel = new FormUpdateModel(form.tileCode, itemId);
        for (let i = 0; i < form.controls.length; i++) {
            const control = form.controls[i];
            model.updatedControls.push({ controlId: control.id, value: control.value, updated: control.updated });
        }
        return model;
    }
}
