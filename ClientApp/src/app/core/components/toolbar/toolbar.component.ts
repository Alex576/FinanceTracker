import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { MatIcon } from "@angular/material/icon";
import { ToolbarService } from './toolbar.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatIcon]
})
export class ToolbarComponent implements OnInit {
  private readonly toolbarService = inject(ToolbarService);

  protected get userName(): string {
    return this.toolbarService.currentUser?.name ?? '';
  }

  constructor() {
    // this.toolbarService.
  }

  ngOnInit() {
  }

}
