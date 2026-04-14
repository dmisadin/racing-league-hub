import { Component, inject } from '@angular/core';
import { Platform, SeasonDto } from '../../models/season.model';
import { RouteService } from '../../../../../core/services/route.service';
import { Game } from '../../../../../shared/models/enums';
import { RouterLink } from "@angular/router";
import { ModalFormParent } from '../../../../../shared/components/modal/modal-form-parent';

@Component({
    selector: 'season-details',
    imports: [RouterLink],
    providers: [RouteService],
    templateUrl: './season-details.component.html'
})
export class SeasonDetailsComponent extends ModalFormParent<SeasonDto> {
    private readonly routeService = inject(RouteService);

    readonly Platform = Platform;
    readonly Game = Game;

    protected override loadDto(): void {
        const leagueSlug = this.routeService.getRouteParam("leagueSlug");
        const seasonSlug = this.routeService.getRouteParam("seasonSlug");

        this.restService.get<SeasonDto>(`/leagues/${leagueSlug}/seasons/${seasonSlug}`)
            .subscribe(res => this.dto.set(res));
    }
}
