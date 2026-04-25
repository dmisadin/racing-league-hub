import { HttpInterceptorFn, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const toastService = inject(ToastService);
    const token = authService.getAccessToken();

    const authReq = token
        ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
        : req;

    return next(authReq).pipe(
        catchError((err: HttpErrorResponse) => {
            if (err.status === HttpStatusCode.Unauthorized && !req.url.includes('/auth/')) {
                return authService.requestRefreshToken().pipe(
                    switchMap(() => {
                        const retryReq = req.clone({
                            setHeaders: { Authorization: `Bearer ${authService.getAccessToken()}` }
                        });
                        return next(retryReq);
                    }),
                    catchError(() => {
                        toastService.showWarning('Your session has expired. Please log in again.');
                        return throwError(() => err);
                    })
                );
            }

            return throwError(() => err);
        })
    );
};