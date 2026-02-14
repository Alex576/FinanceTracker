import { inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly toast = inject(ToastrService);

  notify(message: string): void {
    this.toast.success(message);
  }

  notifyError(message: string): void {
    this.toast.error(message);
  }
}
