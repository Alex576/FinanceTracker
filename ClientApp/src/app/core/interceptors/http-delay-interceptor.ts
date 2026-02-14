import { HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { Observable } from "rxjs";
import { IS_REFRESH_TOKEN } from "../api/authorization-api.service";
import { AuthorizationService } from "../services/authorization.service";
import { HttpTokenHelperService } from "./http-token-helper.service";

export function httpDelayInterception(
    req: HttpRequest<unknown>,
    next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> {
    const httpHelper = inject(HttpTokenHelperService);
    const authService = inject(AuthorizationService);

    if (authService.isRefreshingToken && !req.context.get(IS_REFRESH_TOKEN)) {
        return httpHelper.awaitToken(req, next);
    }
    return next(req);
}