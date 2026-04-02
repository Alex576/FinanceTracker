import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-dashboard-editor',
  templateUrl: './dashboard-editor.component.html',
  styleUrls: ['./dashboard-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardEditorComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
