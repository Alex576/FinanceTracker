import { ChangeDetectionStrategy, Component, input, OnInit } from '@angular/core';
import { GridLayoutEntity } from '../models/layout-editable-item';

@Component({
  selector: 'app-grid-editor',
  templateUrl: './grid-editor.component.html',
  styleUrls: ['./grid-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GridEditorComponent implements OnInit {
  readonly gridEntity = input.required<GridLayoutEntity>();

  constructor() { }

  ngOnInit() {
  }

}
