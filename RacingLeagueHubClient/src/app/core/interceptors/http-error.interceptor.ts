import { HttpInterceptorFn, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ToastService, ToastType } from '../services/toast.service';
import { ProblemDetails } from '../../shared/models/models';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
    const toastService = inject(ToastService);

    return next(req).pipe(
        catchError((err: HttpErrorResponse) => {
            if (err.status !== HttpStatusCode.Unauthorized) {
                toastService.show(resolveMessage(err), resolveType(err));
            }
            return throwError(() => err);
        })
    );
};

function resolveMessage(err: HttpErrorResponse): string {
    const problem = err.error as ProblemDetails;

    if (err.status >= HttpStatusCode.BadRequest 
        && err.status < HttpStatusCode.InternalServerError 
        && problem?.title) {
        return problem.title;
    }

    return getDefaultMessage(err.status);
}

function resolveType(err: HttpErrorResponse): ToastType {
    if (err.status >= HttpStatusCode.InternalServerError ) return 'error';
    if (err.status >= HttpStatusCode.BadRequest) return 'warning';
    return 'info';
}

function getDefaultMessage(status: number): string {
    switch (status) {
        case HttpStatusCode.BadRequest: return 'Invalid request.';
        case HttpStatusCode.Forbidden: return 'You do not have permission to perform this action.';
        case HttpStatusCode.NotFound: return 'The requested resource was not found.';
        case HttpStatusCode.InternalServerError: return 'A server error occurred. Please try again later.';
        default: return 'Something went wrong. Please try again.';
    }
}
