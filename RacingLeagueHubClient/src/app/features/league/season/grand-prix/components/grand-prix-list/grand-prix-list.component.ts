import { Component, input } from '@angular/core';
import { GrandPrixDto } from '../../models/grand-prix.model';
import { ListBase } from '../../../../../../shared/components/list/list-base';
import { RouterLink, RouterOutlet } from "@angular/router";

@Component({
    selector: 'grand-prix-list',
    imports: [RouterLink, RouterOutlet],
    templateUrl: './grand-prix-list.component.html',
})
export class GrandPrixListComponent extends ListBase<GrandPrixDto> {
    leagueSlug = input.required<string>();
    seasonSlug = input.required<string>();
    canEdit = input(false);

    protected override get endpoint(): string {
        return `/leagues/${this.leagueSlug()}/seasons/${this.seasonSlug()}/grands-prix`;
    }

}
