// auth.service.ts — store access token in memory, refresh token in HttpOnly cookie
// OR store both in memory and use silent refresh
import { HttpClient } from '@angular/common/http';
import { Injectable, signal, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap, catchError, EMPTY, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthResponse, LoginRequest, RegisterRequest, UserDto } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly http = inject(HttpClient);
    private readonly router = inject(Router);

    private readonly apiUrl = `${environment.apiUrl}/auth`;

    // ── state ──────────────────────────────────────────────────────────────────
    private readonly currentUser = signal<UserDto | null>(null);
    private readonly accessToken = signal<string | null>(null);
    private readonly accessTokenExpiry = signal<Date | null>(null);
    private refreshToken: string | null = null;  // not a signal — never rendered
    private refreshTimeout: ReturnType<typeof setTimeout> | null = null;

    // ── public selectors ───────────────────────────────────────────────────────
    readonly user = this.currentUser.asReadonly();
    readonly isLoggedIn = computed(() => !!this.currentUser());
    readonly isAdmin = computed(() => this.currentUser()?.isAdmin ?? false);
    readonly driverId = computed(() => this.currentUser()?.driverId ?? null);

    // ── public methods ─────────────────────────────────────────────────────────
    login(payload: LoginRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.apiUrl}/login`, payload).pipe(
            tap(res => this.handleAuthResponse(res))
        );
    }

    register(payload: RegisterRequest): Observable<AuthResponse> {
        return this.http.post<AuthResponse>(`${this.apiUrl}/register`, payload).pipe(
            tap(res => this.handleAuthResponse(res))
        );
    }

    logout(): void {
        if (this.refreshToken) {
            this.http.post(`${this.apiUrl}/revoke`, { refreshToken: this.refreshToken })
                .pipe(catchError(() => EMPTY))
                .subscribe();
        }

        this.clearAuth();
        this.router.navigate(['/login']);
    }

    requestRefreshToken(): Observable<AuthResponse> {
        if (!this.refreshToken) {
            this.clearAuth();
            return EMPTY as any;
        }

        return this.http.post<AuthResponse>(`${this.apiUrl}/refresh`, {
            refreshToken: this.refreshToken
        }).pipe(
            tap(res => this.handleAuthResponse(res)),
            catchError(() => {
                this.clearAuth();
                this.router.navigate(['/login']);
                return EMPTY;
            })
        );
    }

    fetchMe(): Observable<UserDto> {
        return this.http.get<UserDto>(`${this.apiUrl}/me`).pipe(
            tap(user => this.currentUser.set(user))
        );
    }

    getAccessToken(): string | null {
        return this.accessToken();
    }

    isTokenExpired(): boolean {
        const expiry = this.accessTokenExpiry();
        if (!expiry) return true;
        return new Date() >= expiry;
    }

    // ── internals ──────────────────────────────────────────────────────────────
    private handleAuthResponse(res: AuthResponse): void {
        this.accessToken.set(res.accessToken);
        this.accessTokenExpiry.set(new Date(res.accessTokenExpiry));
        this.refreshToken = res.refreshToken;
        this.currentUser.set(res.user);
        this.scheduleTokenRefresh();
    }

    private clearAuth(): void {
        this.accessToken.set(null);
        this.accessTokenExpiry.set(null);
        this.refreshToken = null;
        this.currentUser.set(null);

        if (this.refreshTimeout) {
            clearTimeout(this.refreshTimeout);
            this.refreshTimeout = null;
        }
    }

    private scheduleTokenRefresh(): void {
        if (this.refreshTimeout) clearTimeout(this.refreshTimeout);

        const expiry = this.accessTokenExpiry();
        if (!expiry) return;

        // refresh 30 seconds before expiry
        const msUntilRefresh = expiry.getTime() - Date.now() - 30_000;
        if (msUntilRefresh <= 0) return;

        this.refreshTimeout = setTimeout(() => {
            this.requestRefreshToken().subscribe();
        }, msUntilRefresh);
    }
}