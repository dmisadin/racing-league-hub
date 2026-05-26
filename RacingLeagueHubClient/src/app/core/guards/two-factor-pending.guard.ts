import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";

export const twoFactorPendingGuard: CanActivateFn = () => {
    const authService = inject(AuthService);
    const router = inject(Router);
    
    if (authService.pendingTwoFactorToken())
        return true;

    return router.createUrlTree(['/auth']);
};