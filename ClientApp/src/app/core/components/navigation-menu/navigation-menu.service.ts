import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MenuItem } from '../../models/menu-item';
import { NavigationMenuApiService } from './navigation-menu-api.service';

@Injectable()
export class NavigationMenuService {
  private readonly api = inject(NavigationMenuApiService);


  public getMenuItems(): Observable<MenuItem[]> {
    return this.api.getMenuItems();
  }
}
