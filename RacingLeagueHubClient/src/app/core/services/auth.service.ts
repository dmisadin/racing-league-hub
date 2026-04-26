// auth.service.ts — store access token in memory, refresh token in HttpOnly cookie
// OR store both in memory and use silent refresh
import { HttpClient } from '@angular/common/http';
import { Injectable, signal, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap, catchError, EMPTY, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthResponse, LoginRequest, RegisterRequest, UserDto } from '../models/auth.model';
import { ToastService } from './toast.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly http = inject(HttpClient);
    private readonly router = inject(Router);
    private readonly toastService = inject(ToastService);
    private readonly apiUrl = `${environment.apiUrl}/auth`;

    private readonly _user = signal<UserDto | null>(null);
    private readonly _accessToken = signal<string | null>(null);
    private readonly _accessTokenExpiry = signal<Date | null>(null);
    private readonly _isInitialized = signal<boolean>(false);
    private _refreshTimeout: ReturnType<typeof setTimeout> | null = null;

    readonly user = this._user.asReadonly();
    readonly isLoggedIn = computed(() => !!this._user());
    readonly isAdmin = computed(() => this._user()?.isAdmin ?? false);
    readonly driverId = computed(() => this._user()?.driverId ?? null);
    readonly isInitialized = this._isInitialized.asReadonly(); // public!

    initialize(): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(
            `${this.apiUrl}/refresh`, {}, { withCredentials: true }
        ).pipe(
            tap(res => {
                this.handleAuthResponse(res);
                this._isInitialized.set(true);
            }),
            catchError(() => {
                this._isInitialized.set(true);
                return EMPTY;
            })
        );
    }

    login(payload: LoginRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(
            `${this.apiUrl}/login`, payload, { withCredentials: true }
        ).pipe(
            tap(res => this.handleAuthResponse(res))
        );
    }

    register(payload: RegisterRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(
            `${this.apiUrl}/register`, payload, { withCredentials: true }
        ).pipe(
            tap(res => this.handleAuthResponse(res))
        );
    }

    logout(): void {
        this.http.post(`${this.apiUrl}/revoke`, {}, { withCredentials: true })
            .pipe(catchError(() => EMPTY))
            .subscribe({
                complete: () => {
                    this.toastService.showSuccess('Successfully logged out.');
                    this.clearAuth();
                    this.router.navigate(['/auth/login']);
                }
            });
    }

    requestRefreshToken(): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(
            `${this.apiUrl}/refresh`, {}, { withCredentials: true }
        ).pipe(
            tap(res => this.handleAuthResponse(res)),
            catchError(() => {
                this.clearAuth();
                this.router.navigate(['/auth/login']);
                return EMPTY;
            })
        );
    }

    getAccessToken(): string | null {
        return this._accessToken();
    }

    private handleAuthResponse(res: AuthResponse): void {
        this._accessToken.set(res.accessToken);
        this._accessTokenExpiry.set(new Date(res.accessTokenExpiry));
        this._user.set(res.user);
        this.scheduleTokenRefresh();
    }

    private clearAuth(): void {
        this._accessToken.set(null);
        this._accessTokenExpiry.set(null);
        this._user.set(null);

        if (this._refreshTimeout) {
            clearTimeout(this._refreshTimeout);
            this._refreshTimeout = null;
        }
    }

    private scheduleTokenRefresh(): void {
        if (this._refreshTimeout) clearTimeout(this._refreshTimeout);

        const expiry = this._accessTokenExpiry();
        if (!expiry) return;

        const msUntilRefresh = expiry.getTime() - Date.now() - 30_000;
        if (msUntilRefresh <= 0) return;

        this._refreshTimeout = setTimeout(() => {
            this.requestRefreshToken().subscribe();
        }, msUntilRefresh);
    }
}