import { HttpClient, HttpContext, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable()
export abstract class BaseApiService {
  protected readonly http = inject(HttpClient);
  protected readonly baseUrl = `${environment.baseUrl}api/`;

  constructor() { }

  protected post<T>(
    url: string,
    body?: unknown,
    options?: {
      headers?: HttpHeaders | Record<string, string | string[]>;
      context?: HttpContext;
      observe?: 'body';
      params?: HttpParams | Record<string, string | number | boolean | ReadonlyArray<string | number | boolean>>;
      reportProgress?: boolean;
      responseType?: 'json';
      withCredentials?: boolean;
      credentials?: RequestCredentials;
      keepalive?: boolean;
      priority?: RequestPriority;
      cache?: RequestCache;
      mode?: RequestMode;
      redirect?: RequestRedirect;
      referrer?: string;
      integrity?: string;
      referrerPolicy?: ReferrerPolicy;
      transferCache?: {
        includeHeaders?: string[];
      } | boolean;
      timeout?: number;
    }): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}${url}`, body, options);
  }
}
