import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  private readonly router = inject(Router);

  constructor() { }

  redirectToHome(): void {
    this.router.navigateByUrl('/workplace/home');
  }

  workplaceNavigate(...args: string[]): void {
    this.navigate('/workplace', ...args);
  }

  private navigate(...args: string[]): void {
    this.router.navigateByUrl(args.join('/'));
  }
}
