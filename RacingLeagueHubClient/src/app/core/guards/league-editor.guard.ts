import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";

export const leagueEditorGuard: CanActivateFn = (route) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const leagueSlug = route.parent?.params['leagueSlug']
        ?? route.params['leagueSlug'];

    if (!leagueSlug)
        return router.createUrlTree(['/not-found']);

    if (authService.canEditLeagueSlug(leagueSlug)) 
        return true;

    return router.createUrlTree(['/not-found']);
};