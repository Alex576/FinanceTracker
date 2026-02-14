import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { NavigationMenuComponent } from "../navigation-menu/navigation-menu.component";
import { ToolbarComponent } from "../toolbar/toolbar.component";

@Component({
  selector: 'app-workplace',
  templateUrl: './workplace.component.html',
  styleUrls: ['./workplace.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [NavigationMenuComponent, RouterOutlet, ToolbarComponent]
})
export class WorkplaceComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
