import { HttpInterceptorFn, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '../services/toast.service';
import { ProblemDetails } from '../../shared/models/models';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
    const toastService = inject(ToastService);

    return next(req).pipe(
        catchError((err: HttpErrorResponse) => {
            const problem = err.error as ProblemDetails;
            
            const message = problem?.title ?? getDefaultMessage(err.status);

            // skip 401 — auth interceptor handles those
            if (err.status !== HttpStatusCode.Unauthorized) {
                toastService.showError(message);
            }

            return throwError(() => err);
        })
    );
};

function getDefaultMessage(status: number): string {
    switch (status) {
        case HttpStatusCode.BadRequest: return 'Invalid request.';
        case HttpStatusCode.Forbidden: return 'You do not have permission to perform this action.';
        case HttpStatusCode.NotFound: return 'The requested resource was not found.';
        case HttpStatusCode.InternalServerError: return 'A server error occurred. Please try again later.';
        default: return 'Something went wrong. Please try again.';
    }
}