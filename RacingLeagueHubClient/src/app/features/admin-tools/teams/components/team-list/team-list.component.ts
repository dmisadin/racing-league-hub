import { Component } from "@angular/core";
import { TeamDto } from "../../models/team.model";
import { RouterLink, RouterOutlet } from "@angular/router";
import { ListBase } from "../../../../../shared/components/list/list-base";

@Component({
    selector: 'team-list',
    imports: [RouterLink, RouterOutlet],
    templateUrl: './team-list.component.html'
})
export class TeamListComponent extends ListBase<TeamDto> {
    protected override dtoEndpoint = "/team";

}