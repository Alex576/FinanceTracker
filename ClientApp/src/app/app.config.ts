import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideToastr } from 'ngx-toastr';
import { routes } from './app.routes';
import { errorInterceptor } from './core/interceptors/error-interceptor';
import { httpDelayInterception } from './core/interceptors/http-delay-interceptor';
import { httpInterceptor } from './core/interceptors/http-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(
      withInterceptors([
        errorInterceptor,
        httpDelayInterception,
        httpInterceptor,
      ])
    ),
    provideToastr({
      progressBar: true,
      closeButton: false,
      // extendedTimeOut: 100110,

    }),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
  ]
};
