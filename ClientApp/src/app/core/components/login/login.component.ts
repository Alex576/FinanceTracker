import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatAnchor } from "@angular/material/button";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { from, mergeMap, tap } from 'rxjs';
import { LocalStorageKeys } from '../../models/local-storage-keys';
import { UserModel } from '../../models/user-model';
import { StorageService } from '../../services/storage.service';
import { calculateHash } from '../../utils/helper';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, FormsModule, MatAnchor],
  providers: [LoginService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {
  private readonly storageService = inject(StorageService);
  private readonly loginService = inject(LoginService);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly form: FormGroup<{
    login: FormControl<string>;
    password: FormControl<string>;
  }> = new FormGroup({
    login: new FormControl<string>(null, { validators: [Validators.required, Validators.nullValidator] }),
    password: new FormControl<string>(null, { validators: [Validators.required, Validators.nullValidator] }),
  });
  private readonly formChanged = toSignal(this.form.valueChanges);
  private readonly formValid = toSignal(this.form.statusChanges);

  protected readonly isDisabledLogin = computed<boolean>(() => this.formValid() !== 'VALID');

  constructor() {
    const user = this.storageService.getValue<UserModel>(LocalStorageKeys.CurrentUser);
    if (user) {
      this.loginService.logout(user.id)
        .pipe(
          tap({
            next: () => {
              this.storageService.remove(LocalStorageKeys.Token);
              this.storageService.remove(LocalStorageKeys.CurrentUser);
            }
          }),
          takeUntilDestroyed()
        )
        .subscribe();
    }

  }

  onLoginClick(): void {
    const login = this.form.controls.login;
    const password = this.form.controls.password;
    from(calculateHash(password.value))
      .pipe(
        mergeMap((passwordHash: string) => this.loginService.login(login.value, passwordHash)),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }
}
