export interface TwoFactorSetupResponse {
    manualEntryKey: string;
    otpAuthUri: string;
}

export interface ConfirmTwoFactorRequest {
    code: string;
}

export interface ConfirmTwoFactorResponse {
    recoveryCodes: string[];
}