import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject, signal } from '@angular/core';
import { FormModel } from '../../models/form-editor/form-model';
import { FormValueModel } from '../../models/form-editor/form-value-model';
import { SIDE_PANEL_DATA } from '../../models/side-panel/side-panel-data-token';
import { TileCode } from '../../models/tile-code';
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
    protected readonly destroyRef = inject(DestroyRef);

    protected readonly form = signal<FormModel>(null);
    protected readonly canSaveForm = signal<boolean>(false);

    protected readonly isFormReady = computed<boolean>(() => !!this.form());

    protected get tileCode(): TileCode {
        return this.panelData.tileCode;
    }

    protected getFormUpdateModel(): FormValueModel {
        const form = this.form();
        const model: FormValueModel = new FormValueModel(form.tileCode);
        for (let i = 0; i < form.controls.length; i++) {
            const control = form.controls[i];
            model.updatedControls.push({ controlId: control.id, value: control.value, updated: control.updated });
        }
        return model;
    }

    protected onCanSaveForm(canSave: boolean): void {
        this.canSaveForm.set(canSave);
    }
}
