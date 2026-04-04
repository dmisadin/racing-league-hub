import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';

@Component({
    selector: 'league-list',
    imports: [RouterOutlet, RouterLinkWithHref],
    templateUrl: './league-list.component.html',
    styleUrl: './league-list.component.css',
})
export class LeagueListComponent {

}
