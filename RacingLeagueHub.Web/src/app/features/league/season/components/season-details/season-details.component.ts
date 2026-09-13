import { Component, computed, inject, signal } from '@angular/core';
import { Platform, SeasonDto } from '../../models/season.model';
import { RouteService } from '../../../../../core/services/route.service';
import { Game } from '../../../../../shared/models/enums';
import { RouterLink } from "@angular/router";
import { ModalFormParent } from '../../../../../shared/components/modal/modal-form-parent';
import { GrandPrixListComponent } from '../../grand-prix/components/grand-prix-list/grand-prix-list.component';
import { AuthService } from '../../../../../core/services/auth.service';

@Component({
    selector: 'season-details',
    imports: [RouterLink, GrandPrixListComponent],
    providers: [RouteService],
    templateUrl: './season-details.component.html'
})
export class SeasonDetailsComponent extends ModalFormParent<SeasonDto> {
    private readonly routeService = inject(RouteService);
    private readonly authService = inject(AuthService);

    canEdit = computed(() => {
        const leagueId = this.dto()?.leagueId;
        return leagueId ? this.authService.canEditLeagueId(leagueId) : false;
    });

    leagueSlug = signal<string | null>(null);
    seasonSlug = signal<string | null>(null);

    readonly Platform = Platform;
    readonly Game = Game;

    protected override loadDto(): void {
        this.leagueSlug.set(this.routeService.getRouteParam("leagueSlug"));
        this.seasonSlug.set(this.routeService.getRouteParam("seasonSlug"));

        this.restService.get<SeasonDto>(`/leagues/${this.leagueSlug()}/seasons/${this.seasonSlug()}`)
            .subscribe(res => this.dto.set(res));
    }
}
