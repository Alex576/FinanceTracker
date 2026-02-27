import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormField, MatLabel, MatOption, MatSelect } from "@angular/material/select";
import { BaseSelectComponent } from '../base-select/base-select.component';

@Component({
  selector: 'app-multi-select',
  templateUrl: './multi-select.component.html',
  styleUrls: ['./multi-select.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatSelect, MatLabel, MatFormField, MatOption, ReactiveFormsModule]
})
export class MultiSelectComponent extends BaseSelectComponent<number[]> {

}
