export interface UserDto {
    id: string;
    email: string;
    username: string;
    isAdmin: boolean;
    driverId: string | null;
}

export interface AuthResponse {
    accessToken: string;
    accessTokenExpiry: string;
    user: UserDto;
}

export interface LoginRequest {
    email: string;
    password: string;
    rememberMe: boolean;
}

export interface RegisterRequest {
    username: string;
    email: string;
    password: string;
}

export interface RefreshTokenRequest {
    refreshToken: string;
}