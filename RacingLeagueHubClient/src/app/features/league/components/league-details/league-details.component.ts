import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { RestService } from '../../../../core/services/rest.service';
import { LeagueDto, RegionLabels } from '../../models/league.model';
import { RouteService } from '../../../../core/services/route.service';

@Component({
    selector: 'league-details',
    imports: [RouterOutlet, RouterLinkWithHref],
    providers: [RouteService],
    templateUrl: './league-details.component.html'
})
export class LeagueDetailsComponent implements OnInit {
    private readonly restService = inject(RestService);
    private readonly routeService = inject(RouteService);

    league = signal<LeagueDto | null>(null);

    regionLabels = RegionLabels;

    ngOnInit(): void {
        const leagueSlug = this.routeService.getCurrentRouteParam("leagueSlug");

        this.restService.get<LeagueDto>(`/leagues/${leagueSlug}`).subscribe({
            next: res => this.league.set(res),
            error: () => this.routeService.navigateToNotFoundPage()
        });
    }
}
