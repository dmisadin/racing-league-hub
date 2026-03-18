import { Component, inject, OnInit, signal } from '@angular/core';
import { TeamFormComponent } from "../team-form/team-form.component";
import { RouteService } from '../../../../../core/services/route.service';
import { TeamDto } from '../../models/team.model';
import { RouterOutlet, RouterLinkWithHref } from "@angular/router";
import { GameTeamFormComponent } from "../game-team-form/game-team-form.component";
import { Game } from '../../../../../shared/models/enums';
import { ModalFormParent } from '../../../../../shared/components/modal/modal-form-parent';

@Component({
    selector: 'team-details',
    imports: [TeamFormComponent, RouterOutlet, GameTeamFormComponent, RouterLinkWithHref],
    providers: [RouteService],
    templateUrl: './team-details.component.html'
})
export class TeamDetailsComponent extends ModalFormParent<TeamDto> implements OnInit {
    private readonly routeService = inject(RouteService);
    teamId = signal<number | null>(null);

    Game = Game;

    ngOnInit(): void {
        const teamId = this.routeService.getRouteParam('teamId');
        if (!teamId)
            return;

        this.teamId.set(Number(teamId));
    }

    protected override loadDto() {
        this.restService.get<TeamDto>(`/team/get-by-id/${this.teamId()}`).subscribe(res => {
            this.dto.set(res);
        });
    }
}
