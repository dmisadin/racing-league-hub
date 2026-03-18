import { Component, inject } from "@angular/core";
import { RouterLink, RouterOutlet } from "@angular/router";
import { TrackDto } from "../../models/track.model";
import { CountryService } from "../../../../../core/services/country.service";
import { FlagUrlPipe } from "../../../../../shared/pipes/flag-url.pipe";
import { ListBase } from "../../../../../shared/components/list/list-base";

@Component({
    selector: 'track-list',
    templateUrl: './track-list.component.html',
    imports: [RouterLink, RouterOutlet, FlagUrlPipe],
})
export class TrackListComponent extends ListBase<TrackDto> {
    protected override dtoEndpoint = "track";
    countryService = inject(CountryService);
}
