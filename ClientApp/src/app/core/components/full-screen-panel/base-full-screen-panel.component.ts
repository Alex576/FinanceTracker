import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject, signal } from "@angular/core";
import { FullScreenFormEditorModel } from "../../models/full-screen-form-editor/full-screen-form-editor-model";
import { FULL_SCREEN_PANEL_DATA } from "../../models/full-screen-panel/full-screen-panel-token";
import { TileCode } from "../../models/tile-code";
import { FullScreenPanelService } from "../../services/full-screen-panel.service";

@Component({
    selector: 'app-base-full-screen-panel',
    template: ``,
    styleUrls: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export abstract class BaseFullScreenPanelComponent {
    protected readonly panelData = inject(FULL_SCREEN_PANEL_DATA);
    protected readonly fullScreenPanelService = inject(FullScreenPanelService);
    protected readonly destroyRef = inject(DestroyRef);

    protected readonly form = signal<FullScreenFormEditorModel>(null);
    protected readonly canSaveForm = signal<boolean>(false);

    protected readonly isFormReady = computed<boolean>(() => !!this.form());

    protected get tileCode(): TileCode {
        return this.panelData.tileCode;
    }

    // protected getFormUpdateModel(): FormValueModel {
    //     const form = this.form();
    //     const model: FormValueModel = new FormValueModel(form.tileCode);
    //     for (let i = 0; i < form.controls.length; i++) {
    //         const control = form.controls[i];
    //         model.updatedControls.push({ controlId: control.id, value: control.value, updated: control.updated });
    //     }
    //     return model;
    // }

    protected onCanSaveForm(canSave: boolean): void {
        this.canSaveForm.set(canSave);
    }
}