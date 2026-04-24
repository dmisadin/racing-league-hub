export interface UserDto {
    id: string;
    email: string;
    username: string;
    isAdmin: boolean;
    driverId: string | null;
}

export interface AuthResponse {
    accessToken: string;
    refreshToken: string;
    accessTokenExpiry: string; // ISO date string
    user: UserDto;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    username: string;
    email: string;
    password: string;
}

export interface RefreshTokenRequest {
    refreshToken: string;
}