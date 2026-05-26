export interface TwoFactorSetupResponse {
    manualEntryKey: string;
    otpAuthUri: string;
}

export interface ConfirmTwoFactorRequest {
    code: string;
}