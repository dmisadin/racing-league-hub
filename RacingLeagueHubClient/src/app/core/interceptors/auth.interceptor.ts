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
        ? req.clone({
            withCredentials: true,
            setHeaders: { Authorization: `Bearer ${token}` }
        })
        : req.clone({ withCredentials: true });

    return next(authReq).pipe(
        catchError((err: HttpErrorResponse) => {
            const isAuthRoute = req.url.includes('/auth/');
            const isUnauthorized = err.status === HttpStatusCode.Unauthorized;

            if (isUnauthorized && !isAuthRoute) {
                console.log("test")
                return authService.requestRefreshToken().pipe(
                    switchMap(() => {
                        const retryReq = req.clone({
                            withCredentials: true,
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