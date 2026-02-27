import { inject, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToolCodeUrlMap } from '../models/menu-item';
import { LastSessionSetting } from '../models/user-settings/last-session-setting';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);

  constructor() { }

  redirectToHome(): void {
    this.router.navigateByUrl('/workplace/dashboard');
  }

  // workplaceNavigate(...args: string[]): void {
  //   this.navigate(...args);
  // }

  navigateToLoginPage() {
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.url } });
  }

  navigateDefaultOrReturnUrl(): void {
    const returnUrl: string = this.activeRoute.snapshot.queryParams['returnUrl'];
    if (returnUrl) {
      this.router.navigate([returnUrl]);
    }
    else {
      this.redirectToHome();
    }
  }

  validateUrl(sessionSetting: LastSessionSetting): void {
    const targetUrl = ToolCodeUrlMap.get(sessionSetting.lastOpenedTool);
    if (!targetUrl) { return; }

    if (targetUrl != this.router.url.split('?')[0]) {
      this.navigate(targetUrl);
    }
  }

  navigate(...args: string[]): void {
    this.router.navigateByUrl(args.join('/'));
  }
}
