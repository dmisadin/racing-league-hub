import { Component, computed, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { map } from 'rxjs';
import { RouteService } from '../../../../../core/services/route.service';
import { AuthService } from '../../../../../core/services/auth.service';
import { ModalFormParent } from '../../../../../shared/components/modal/modal-form-parent';
import { SeasonDetailsDto } from '../../models/season.model';
import { GrandPrixListComponent } from '../../grand-prix/components/grand-prix-list/grand-prix-list.component';

@Component({
    selector: 'season-division-details',
    imports: [RouterLink, GrandPrixListComponent],
    providers: [RouteService],
    templateUrl: './season-division-details.component.html'
})
export class SeasonDivisionDetailsComponent extends ModalFormParent<SeasonDetailsDto> {
    private readonly routeService = inject(RouteService);
    private readonly authService = inject(AuthService);
    private readonly activatedRoute = inject(ActivatedRoute);

    leagueSlug = signal<string | null>(null);
    seasonSlug = signal<string | null>(null);

    // Switching between divisions reuses this component, so follow the param instead of the snapshot.
    divisionSlug = toSignal(
        this.activatedRoute.paramMap.pipe(map(params => params.get('divisionSlug'))),
        { initialValue: this.activatedRoute.snapshot.paramMap.get('divisionSlug') }
    );

    division = computed(() => this.dto()?.divisions.find(d => d.division.slug === this.divisionSlug()) ?? null);

    /** Other divisions of the same season, for quick switching. */
    otherDivisions = computed(() => this.dto()?.divisions.filter(d => d.division.slug !== this.divisionSlug()) ?? []);

    canEdit = computed(() => {
        const leagueId = this.dto()?.season.leagueId;
        return leagueId ? this.authService.canEditLeagueId(leagueId) : false;
    });

    protected override loadDto(): void {
        this.leagueSlug.set(this.routeService.getRouteParam("leagueSlug"));
        this.seasonSlug.set(this.routeService.getRouteParam("seasonSlug"));

        this.restService.get<SeasonDetailsDto>(`/leagues/${this.leagueSlug()}/seasons/${this.seasonSlug()}/details`)
            .subscribe({
                next: res => {
                    if (!res.divisions.some(d => d.division.slug === this.divisionSlug())) {
                        this.routeService.navigateToNotFoundPage();
                        return;
                    }

                    this.dto.set(res);
                },
                error: () => this.routeService.navigateToNotFoundPage()
            });
    }
}
