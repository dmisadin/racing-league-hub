import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { TeamFormComponent } from "../team-form/team-form.component";
import { RestService } from '../../../../../core/services/rest.service';
import { RouteService } from '../../../../../core/services/route.service';
import { TeamDto } from '../../models/team.model';
import { RouterOutlet, RouterLinkWithHref } from "@angular/router";
import { GameTeamFormComponent } from "../game-team-form/game-team-form.component";
import { Game } from '../../../../../shared/models/enums';
import { ListService } from '../../../../../shared/services/list.service';

@Component({
    selector: 'team-details',
    imports: [TeamFormComponent, RouterOutlet, GameTeamFormComponent, RouterLinkWithHref],
    providers: [RouteService],
    templateUrl: './team-details.component.html'
})
export class TeamDetailsComponent implements OnInit {
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);
    private readonly listService = inject(ListService);
    team = signal<TeamDto | null>(null);
    teamId = signal<number | null>(null);

    Game = Game;

    constructor() {
        effect(() => {
            this.listService.refresh();
            this.loadTeam();
        });
    }

    ngOnInit(): void {
        const teamId = this.routeService.getRouteParam('teamId');
        if (!teamId)
            return;

        this.teamId.set(Number(teamId));
        this.loadTeam();
    }

    private loadTeam() {
        this.restService.get<TeamDto>(`/team/get-by-id/${this.teamId()}`).subscribe(res => {
            this.team.set(res);
        });
    }
}
