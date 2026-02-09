import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeSwitcherService {

  private readonly toggleThemeSub$ = new Subject<void>();
  readonly toggleTheme$ = this.toggleThemeSub$.asObservable();

  constructor() { }

  onToggleTheme(): void {
    this.toggleThemeSub$.next();
  }

}
