import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RestService } from '../../../../core/services/rest.service';
import { LeagueDto } from '../../models/league.model';
import { RouteService } from '../../../../core/services/route.service';
import { JsonPipe } from '@angular/common';

@Component({
    selector: 'league-details',
    imports: [RouterOutlet, JsonPipe],
    providers: [RouteService],
    templateUrl: './league-details.component.html'
})
export class LeagueDetailsComponent implements OnInit {
    private readonly restService = inject(RestService);
    private readonly routeService = inject(RouteService);

    league = signal<LeagueDto | null>(null);

    ngOnInit(): void {
        const leagueSlug = this.routeService.getCurrentRouteParam("leagueSlug");

        this.restService.get<LeagueDto>(`/league/get-by-slug/${leagueSlug}`).subscribe({
            next: res => this.league.set(res),
            error: () => this.routeService.navigateToNotFoundPage()
        });
    }
}
