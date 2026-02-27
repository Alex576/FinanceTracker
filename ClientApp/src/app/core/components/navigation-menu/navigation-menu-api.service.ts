import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MenuItem } from '../../models/menu-item';
import { BaseApiService } from '../../services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class NavigationMenuApiService extends BaseApiService {
  private readonly GET_MENU_ITEMS = 'Menu/GetMenuItems';

  public getMenuItems(): Observable<MenuItem[]> {
    return this.http.get<MenuItem[]>(`${this.baseUrl}${this.GET_MENU_ITEMS}`);
  }
}
