import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { DestroyRef, inject, Injectable } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter, Observable, Subject, switchMap, take, takeUntil } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpTokenHelperService {
  private readonly destroyRef = inject(DestroyRef);
  public isTokenRefreshing = false;
  private readonly awaitingRequests = new Set<HttpRequest<unknown>>();

  private readonly refreshTokenSub$ = new Subject<string>();
  public readonly refreshToken$ = this.refreshTokenSub$.asObservable();

  private readonly cancelSub$ = new Subject<void>();
  public readonly cancel$ = this.cancelSub$.asObservable();
  test: Observable<HttpEvent<unknown>>;

  public refreshToken(token: string): void {
    this.refreshTokenSub$.next(token);
  }

  public cancelRequests(): void {
    this.cancelSub$.next();
    // this.awaitingRequests.clear();
  }

  // public isAwaitingToken(req: HttpRequest<unknown>): boolean {
  //   return this.awaitingRequests.has(req);
  // }

  // public ignoreAwaiting(req: HttpRequest<unknown>): void {
  //   this.awaitingRequests.add(req);
  // }

  public awaitToken(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
    // this.awaitingRequests.add(req);

    return this.refreshToken$
      .pipe(
        filter(token => !!token),
        take(1),
        switchMap(() => {
          return next(req);
        }),
        takeUntil(this.cancel$),
        takeUntilDestroyed(this.destroyRef),
      );
  }
}
