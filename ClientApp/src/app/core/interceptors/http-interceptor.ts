import { HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { Observable } from "rxjs";
import { IS_REFRESH_TOKEN } from "../api/authorization-api.service";
import { LocalStorageKeys } from "../models/local-storage-keys";
import { StorageService } from "../services/storage.service";


export function httpInterceptor(
    req: HttpRequest<unknown>,
    next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> {
    if (req.context.get(IS_REFRESH_TOKEN) || req.headers.has('Authorization')) {
        return next(req);
    }
    const storageService = inject(StorageService);
    const token = storageService.getValue(LocalStorageKeys.Token);
    if (token) {
        return updateToken();
    }
    return next(req);

    function updateToken() {
        const clone = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${token}`)
        });
        return next(clone);
    }
}