import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { catchError, Observable, switchMap, throwError } from "rxjs";
import { LocalStorageKeys } from "../models/local-storage-keys";
import { AuthorizationService } from "../services/authorization.service";
import { NavigationService } from "../services/navigation.service";
import { NotificationService } from "../services/notification.service";
import { StorageService } from "../services/storage.service";
import { HttpTokenHelperService } from "./http-token-helper.service";

export function errorInterceptor(
    req: HttpRequest<unknown>,
    next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> {
    const navigationService = inject(NavigationService);
    const notificationService = inject(NotificationService);
    const storageService = inject(StorageService);
    const authService = inject(AuthorizationService);
    const httpHelper = inject(HttpTokenHelperService);

    return handleRequestErrors(req, next);

    function handleRequestErrors(
        req: HttpRequest<unknown>,
        next: HttpHandlerFn,
    ): Observable<HttpEvent<unknown>> {
        return next(req)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    if (error.status === 401) {
                        const token = storageService.getValue(LocalStorageKeys.Token);
                        if (token) {
                            return tryUpdateToken(req, next, token);
                        }
                        notificationService.notifyError("Session is over");
                        navigationService.navigateToLoginPage();
                        return throwError(() => error);
                    }
                    notificationService.notifyError("Unknown Error");
                    console.error(error.message);

                    return throwError(() => error);
                })
            );
    }

    function tryUpdateToken(
        req: HttpRequest<unknown>,
        next: HttpHandlerFn,
        token: string,
    ): Observable<HttpEvent<unknown>> {
        if (authService.isRefreshingToken) {
            return httpHelper.awaitToken(req, next);
        }
        else {
            return authService.refreshToken(token)
                .pipe(
                    catchError((err) => {
                        httpHelper.cancelRequests();
                        storageService.remove(LocalStorageKeys.Token);
                        navigationService.navigateToLoginPage();
                        return throwError(() => err);
                    }),
                    switchMap(({ accessToken }) => {
                        storageService.saveValue(LocalStorageKeys.Token, accessToken);
                        httpHelper.refreshToken(accessToken);
                        return handleRequestErrors(req, next);
                    }),
                );
        }
    }
}
