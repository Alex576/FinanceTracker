// import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
// import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
// import { MatButtonModule } from '@angular/material/button';
// import { tap } from 'rxjs';
// import { FormControl } from '../../../../models/controls/form-control';
// import { EditorType } from '../../../../models/form-editor/editor-type';
// import { OperationResult, OperationResultData } from '../../../../models/operation-result/operation-result';
// import { isSuccess } from '../../../../models/operation-result/result-code';
// import { FormComponent } from "../../../form/form.component";
// import { LayoutEditorService } from '../../../layout-editor/layout-editor.service';
// import { LayoutEditorModel } from '../../../layout-editor/models/layout-editor-model';
// import { BaseSidePanelComponent } from '../../base-side-panel.component';
// import { LayoutItemEditorService } from '../../layout-item-editor.service';
// import { SidePanelViewComponent } from "../../side-panel-view/side-panel-view.component";

// @Component({
//   selector: 'app-filter-editor',
//   templateUrl: './filter-editor.component.html',
//   styleUrls: ['./filter-editor.component.scss'],
//   changeDetection: ChangeDetectionStrategy.OnPush,
//   imports: [SidePanelViewComponent, FormComponent, MatButtonModule],
//   providers: [LayoutItemEditorService],
// })
// export class FilterEditorComponent extends BaseSidePanelComponent {
//   private readonly service = inject(LayoutItemEditorService);
//   private readonly layoutService = inject(LayoutEditorService);

//   private get data(): FormControl {
//     return this.panelData.data as FormControl;
//   }

//   constructor() {
//     super();

//     this.service.getForm({ tileCode: this.panelData.tileCode, itemId: this.data.id, type: EditorType.Filter })
//       .pipe(
//         tap({ next: (form) => this.form.set(form) }),
//         takeUntilDestroyed(),
//       )
//       .subscribe();
//   }

//   onFormChanged(): void {
//     this.service.updateForm(this.getFormUpdateModel(this.data.id, EditorType.Filter))
//       .pipe(
//         tap({ next: (form) => this.form.set(form) }),
//         takeUntilDestroyed(this.destroyRef),
//       )
//       .subscribe();
//   }

//   onDelete(): void {
//     this.service.deleteItem({ tileCode: this.panelData.tileCode, itemId: this.data.id, type: EditorType.Filter })
//       .pipe(
//         takeUntilDestroyed(this.destroyRef),
//       )
//       .subscribe({
//         next: ({ code }: OperationResult) => {
//           if (isSuccess(code)) {
//             this.layoutService.removeLayoutElement(this.panelData.tileCode, this.data.id);
//             this.sidePanelService.close();
//           }
//         }
//       });
//   }

//   onSave(): void {
//     this.service.saveForm(this.getFormUpdateModel(this.data.id, EditorType.Filter))
//       .pipe(
//         takeUntilDestroyed(this.destroyRef),
//       )
//       .subscribe({
//         next: (result: OperationResultData<LayoutEditorModel>) => {
//           if (isSuccess(result.code)) {
//             this.layoutService.applyEditorLayout(result.result);
//             this.sidePanelService.close();
//           }
//         }
//       });
//   }
// }
