import { Component, computed, inject } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { LeagueDto, RegionLabels } from '../../models/league.model';
import { RouteService } from '../../../../core/services/route.service';
import { SeasonListComponent } from "../../season/components/season-list/season-list.component";
import { ModalFormParent } from '../../../../shared/components/modal/modal-form-parent';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
    selector: 'league-details',
    imports: [RouterOutlet, RouterLinkWithHref, SeasonListComponent],
    providers: [RouteService],
    templateUrl: './league-details.component.html'
})
export class LeagueDetailsComponent extends ModalFormParent<LeagueDto> {
    private readonly routeService = inject(RouteService);
    private readonly authService = inject(AuthService);

    canEdit = computed(() => {
        const leagueId = this.dto()?.id;
        return leagueId ? this.authService.canEditLeagueId(leagueId) : false;
    });

    regionLabels = RegionLabels;

    protected override loadDto(): void {
        const leagueSlug = this.routeService.getCurrentRouteParam("leagueSlug");

        this.restService.get<LeagueDto>(`/leagues/${leagueSlug}`).subscribe({
            next: res => this.dto.set(res),
            error: () => this.routeService.navigateToNotFoundPage()
        });
    }
}
