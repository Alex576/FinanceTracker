import { ChangeDetectionStrategy, Component } from '@angular/core';
import { EditorType } from '../../../models/form-editor/editor-type';
import { BaseSidePanelComponent } from '../base-side-panel.component';
import { LayoutItemFormEditorModel } from './item-editor/layout-item-form-editor-model';

@Component({
    selector: 'app-base-layout-side-side-panel',
    template: ``,
    styleUrls: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export abstract class BaseLayoutItemSidePanelComponent extends BaseSidePanelComponent {

    protected getLayoutItemFormUpdateModel(itemId: string, type: EditorType = EditorType.Form): LayoutItemFormEditorModel {
        const updateModel = this.getFormUpdateModel();
        return { tileCode: updateModel.tileCode, itemId, type, formValueModel: updateModel };
    }
}
