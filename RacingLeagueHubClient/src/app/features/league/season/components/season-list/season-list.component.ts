import { Component, input } from '@angular/core';
import { ListBase } from '../../../../../shared/components/list/list-base';
import { SeasonDto } from '../../models/season.model';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'season-list',
    imports: [RouterLink],
    templateUrl: './season-list.component.html'
})
export class SeasonListComponent extends ListBase<SeasonDto> {
    leagueSlug = input.required<string>();

    protected override get endpoint(): string {
        return `/leagues/${this.leagueSlug()}/seasons`;
    }
}
