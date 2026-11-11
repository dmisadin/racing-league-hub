import { Component, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink, RouterOutlet } from "@angular/router";
import { grandPrixApiPath, GrandPrixDto } from '../../models/grand-prix.model';
import { ListBase } from '../../../../../../shared/components/list/list-base';
import { FlagUrlPipe } from '../../../../../../shared/pipes/flag-url.pipe';

@Component({
    selector: 'grand-prix-list',
    imports: [RouterLink, RouterOutlet, DatePipe, FlagUrlPipe],
    templateUrl: './grand-prix-list.component.html',
})
export class GrandPrixListComponent extends ListBase<GrandPrixDto> {
    leagueSlug = input.required<string>();
    seasonSlug = input.required<string>();
    divisionSlug = input<string | null>(null);
    canEdit = input(false);

    protected override get endpoint(): string {
        return grandPrixApiPath(this.leagueSlug(), this.seasonSlug(), this.divisionSlug());
    }

    isPast(gp: GrandPrixDto): boolean {
        return new Date(gp.startingAt).getTime() <= Date.now();
    }
}
