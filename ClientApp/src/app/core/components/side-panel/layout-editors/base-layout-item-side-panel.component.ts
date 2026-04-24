import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BaseSidePanelComponent } from '../base-side-panel.component';
import { LayoutItemFormEditorModel } from './item-editor/layout-item-form-editor-model';

@Component({
    selector: 'app-base-layout-side-side-panel',
    template: ``,
    styleUrls: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export abstract class BaseLayoutItemSidePanelComponent extends BaseSidePanelComponent {

    protected getLayoutItemFormUpdateModel(itemId: string): LayoutItemFormEditorModel {
        const updateModel = this.getFormUpdateModel();
        return { tileCode: updateModel.tileCode, itemId, formValueModel: updateModel };
    }
}
