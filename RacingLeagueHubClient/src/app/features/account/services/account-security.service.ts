import { Injectable, inject } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { TwoFactorSetupResponse, ConfirmTwoFactorRequest } from "../models/2fa.model";
import { RestService } from "../../../core/services/rest.service";

@Injectable({
    providedIn: 'root'
})
export class AccountSecurityService {
    private readonly restService = inject(RestService);

    setupTwoFactor(): Observable<TwoFactorSetupResponse> {
        return this.restService.post<TwoFactorSetupResponse>('/account/2fa/setup', {});
    }

    confirmTwoFactor(payload: ConfirmTwoFactorRequest): Observable<void> {
        return this.restService.post<void>('/account/2fa/confirm', payload);
    }
}