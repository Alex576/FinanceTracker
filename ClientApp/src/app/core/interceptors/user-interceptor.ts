import { HttpEvent, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { Observable } from "rxjs";
import { ToolbarService } from "../components/toolbar/toolbar.service";

export function userInterception(
    req: HttpRequest<unknown>,
    next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> {
    const toolbarService = inject(ToolbarService);

    const userId = toolbarService.currentUser?.id?.toString() ?? '';
    if (!userId) {
        return next(req);
    }

    const clone = req.clone({
        headers: req.headers.set('userId', userId)
    });
    return next(clone);
}