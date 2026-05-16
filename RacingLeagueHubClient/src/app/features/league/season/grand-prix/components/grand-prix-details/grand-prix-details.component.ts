import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { RestService } from '../../../../../../core/services/rest.service';
import { RouteService } from '../../../../../../core/services/route.service';
import { ListService } from '../../../../../../shared/services/list.service';
import { GrandPrixDto } from '../../models/grand-prix.model';
import { DatePipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { AuthService } from '../../../../../../core/services/auth.service';

@Component({
    selector: 'grand-prix-details',
    imports: [DatePipe, RouterLink],
    providers: [RouteService],
    templateUrl: './grand-prix-details.component.html',
})
export class GrandPrixDetailsComponent implements OnInit {
    private readonly routeService = inject(RouteService);
    protected readonly restService = inject(RestService);
    protected readonly listService = inject(ListService);
    private readonly authService = inject(AuthService);

    canEdit = computed(() => {
        const leagueId = this.dto()?.leagueId;
        return leagueId ? this.authService.canEditLeagueId(leagueId) : false;
    });

    protected dto = signal<GrandPrixDto | null>(null);
    leagueSlug = signal<string | null>(null);
    seasonSlug = signal<string | null>(null);
    grandPrixSlug = signal<string | null>(null);

    ngOnInit(): void {
        this.loadDto();
    }

    protected loadDto(): void {
        this.leagueSlug.set(this.routeService.getRouteParam("leagueSlug"));
        this.seasonSlug.set(this.routeService.getRouteParam("seasonSlug"));
        this.grandPrixSlug.set(this.routeService.getRouteParam("grandPrixSlug"));

        this.restService.get<GrandPrixDto>(`/leagues/${this.leagueSlug()}/seasons/${this.seasonSlug()}/grands-prix/${this.grandPrixSlug()}`)
            .subscribe(res => this.dto.set(res));
    }
}
