// auth.service.ts — store access token in memory, refresh token in HttpOnly cookie
// OR store both in memory and use silent refresh
import { HttpClient } from '@angular/common/http';
import { Injectable, signal, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap, catchError, EMPTY, Observable, throwError, of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthResponse, ForgotPasswordRequest, LoginRequest, RegisterRequest, ResetPasswordRequest, UserDto } from '../models/auth.model';
import { ToastService } from './toast.service';
import { LeagueRole } from '../../features/league/models/league-roles.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly http = inject(HttpClient);
    private readonly router = inject(Router);
    private readonly toastService = inject(ToastService);
    private readonly apiUrl = `${environment.apiUrl}/auth`;

    private readonly hadSessionKey = 'racing_league_had_session';

    private readonly _user = signal<UserDto | null>(null);
    private readonly _leagueRoles = signal<LeagueRole[]>([]);
    private readonly _accessToken = signal<string | null>(null);
    private readonly _accessTokenExpiry = signal<Date | null>(null);
    private readonly _isInitialized = signal<boolean>(false);

    private _refreshTimeout: ReturnType<typeof setTimeout> | null = null;

    readonly user = this._user.asReadonly();
    readonly isLoggedIn = computed(() => !!this._user());
    readonly isAdmin = computed(() => this._user()?.isAdmin ?? false);
    readonly driverId = computed(() => this._user()?.driverId ?? null);
    readonly isInitialized = this._isInitialized.asReadonly();

    initialize(): Observable<AuthResponse | null> {
        if (!this.hadSession()) {
            this._isInitialized.set(true);
            return of(null);
        }

        return this.http.post<AuthResponse>(`${this.apiUrl}/refresh`, {}, { withCredentials: true })
            .pipe(
                tap(res => {
                    this.handleAuthResponse(res);
                    this._isInitialized.set(true);
                }),
                catchError(() => {
                    this.clearAuth();
                    this._isInitialized.set(true);
                    return of(null);
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

    requestRefreshToken(options?: { redirectOnFailure?: boolean }): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(
            `${this.apiUrl}/refresh`, {}, { withCredentials: true }
        ).pipe(
            tap(res => this.handleAuthResponse(res)),
            catchError(err => {
                this.clearAuth();

                if (options?.redirectOnFailure) {
                    this.router.navigate(['/auth/login']);
                }

                return throwError(() => err);
            })
        );
    }

    getAccessToken(): string | null {
        return this._accessToken();
    }

    shouldTryRefresh(): boolean {
        return !!this._accessToken() || this.hadSession();
    }

    forgotPassword(payload: ForgotPasswordRequest): Observable<void> {
        return this.http.post<void>(`${this.apiUrl}/forgot-password`, payload);
    }

    resetPassword(payload: ResetPasswordRequest): Observable<void> {
        return this.http.post<void>(`${this.apiUrl}/reset-password`, payload);
    }

    loadLeagueRoles(): void {
        if (!this._accessToken()) return;

        this.http.get<LeagueRole[]>(`${this.apiUrl}/me/league-roles`)
            .pipe(catchError(() => EMPTY))
            .subscribe(roles => {
                this._leagueRoles.set(roles);
            });
    }

    getLeagueRole(leagueId: string): LeagueRole | undefined {
        return this._leagueRoles().find(r => r.leagueId === leagueId);
    }

    canEditLeagueSlug(leagueSlug: string): boolean {
        const role = this._leagueRoles().find(r => r.leagueSlug === leagueSlug);
        return role ? this.canEditLeague(role) : false;
    }

    canEditLeagueId(leagueId: string): boolean {
        const role = this._leagueRoles().find(r => r.leagueId === leagueId);
        return role ? this.canEditLeague(role) : false;
    }

    private canEditLeague(role: LeagueRole): boolean {
        return role.isOwner || role.isAdmin || role.isEditor;
    }

    private handleAuthResponse(res: AuthResponse): void {
        this._accessToken.set(res.accessToken);
        this._accessTokenExpiry.set(new Date(res.accessTokenExpiry));
        this._user.set(res.user);

        localStorage.setItem(this.hadSessionKey, 'true');

        this.scheduleTokenRefresh();
        this.loadLeagueRoles();
    }

    private clearAuth(): void {
        this._accessToken.set(null);
        this._accessTokenExpiry.set(null);
        this._user.set(null);
        this._leagueRoles.set([]);

        localStorage.removeItem(this.hadSessionKey);

        if (this._refreshTimeout) {
            clearTimeout(this._refreshTimeout);
            this._refreshTimeout = null;
        }
    }

    private hadSession(): boolean {
        return localStorage.getItem(this.hadSessionKey) === 'true';
    }

    private scheduleTokenRefresh(): void {
        if (this._refreshTimeout)
            clearTimeout(this._refreshTimeout);

        const expiry = this._accessTokenExpiry();

        if (!expiry) return;

        const msUntilRefresh = expiry.getTime() - Date.now() - 30_000;

        if (msUntilRefresh <= 0) return;

        this._refreshTimeout = setTimeout(() => {
            this.requestRefreshToken().subscribe();
        }, msUntilRefresh);
    }
}
