import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const token = authService.getAccessToken();

    const authReq = token
        ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
        : req;

    return next(authReq).pipe(
        catchError((err: HttpErrorResponse) => {
            // attempt silent refresh on 401, then replay the original request once
            if (err.status === 401 && !req.url.includes('/auth/')) {
                return authService.requestRefreshToken().pipe(
                    switchMap(() => {
                        const retryReq = req.clone({
                            setHeaders: { Authorization: `Bearer ${authService.getAccessToken()}` }
                        });
                        return next(retryReq);
                    })
                );
            }

            return throwError(() => err);
        })
    );
};