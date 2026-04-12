import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { ListBase } from '../../../../shared/components/list/list-base';
import { LeagueDto } from '../../models/league.model';

@Component({
    selector: 'league-list',
    imports: [RouterOutlet, RouterLinkWithHref],
    templateUrl: './league-list.component.html'
})
export class LeagueListComponent extends ListBase<LeagueDto>{
    protected override dtoEndpoint = "/leagues";
}
