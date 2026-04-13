import { Component, inject, OnInit, signal } from '@angular/core';
import { Platform, SeasonDto } from '../../models/season.model';
import { RestService } from '../../../../../core/services/rest.service';
import { RouteService } from '../../../../../core/services/route.service';
import { Game } from '../../../../../shared/models/enums';
import { RouterLink } from "@angular/router";

@Component({
    selector: 'season-details',
    imports: [RouterLink],
    providers: [RouteService],
    templateUrl: './season-details.component.html'
})
export class SeasonDetailsComponent implements OnInit {
    private readonly restService = inject(RestService);
    private readonly routeService = inject(RouteService);

    season = signal<SeasonDto | null>(null);

    readonly Platform = Platform;
    readonly Game = Game;

    ngOnInit(): void {
        const leagueSlug = this.routeService.getRouteParam("leagueSlug");
        const seasonSlug = this.routeService.getRouteParam("seasonSlug");

        this.restService.get<SeasonDto>(`/leagues/${leagueSlug}/seasons/${seasonSlug}`)
                        .subscribe(res => this.season.set(res));
    }
}
