import { Component, computed, inject, signal } from '@angular/core';
import { RouterLink } from "@angular/router";
import { Platform, SeasonDetailsDto } from '../../models/season.model';
import { RouteService } from '../../../../../core/services/route.service';
import { AuthService } from '../../../../../core/services/auth.service';
import { ModalFormParent } from '../../../../../shared/components/modal/modal-form-parent';
import { GameLabelPipe } from '../../../../../shared/pipes/game-label.pipe';
import { GrandPrixListComponent } from '../../grand-prix/components/grand-prix-list/grand-prix-list.component';
import { SeasonSettingsComponent } from '../season-settings/season-settings.component';
import { SeasonDivisionCardComponent } from '../season-division-card/season-division-card.component';

@Component({
    selector: 'season-details',
    imports: [RouterLink, GrandPrixListComponent, SeasonSettingsComponent, SeasonDivisionCardComponent, GameLabelPipe],
    providers: [RouteService],
    templateUrl: './season-details.component.html'
})
export class SeasonDetailsComponent extends ModalFormParent<SeasonDetailsDto> {
    private readonly routeService = inject(RouteService);
    private readonly authService = inject(AuthService);

    canEdit = computed(() => {
        const leagueId = this.dto()?.season.leagueId;
        return leagueId ? this.authService.canEditLeagueId(leagueId) : false;
    });

    /** With several divisions each one has its own calendar and settings, shown on its own page. */
    hasMultipleDivisions = computed(() => (this.dto()?.divisions.length ?? 0) > 1);

    leagueSlug = signal<string | null>(null);
    seasonSlug = signal<string | null>(null);

    readonly Platform = Platform;

    protected override loadDto(): void {
        this.leagueSlug.set(this.routeService.getRouteParam("leagueSlug"));
        this.seasonSlug.set(this.routeService.getRouteParam("seasonSlug"));

        this.restService.get<SeasonDetailsDto>(`/leagues/${this.leagueSlug()}/seasons/${this.seasonSlug()}/details`)
            .subscribe({
                next: res => this.dto.set(res),
                error: () => this.routeService.navigateToNotFoundPage()
            });
    }
}
