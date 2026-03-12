import { Component, inject, OnInit, signal } from "@angular/core";
import { RouterLink, RouterOutlet } from "@angular/router";
import { RestService } from "../../../../../core/services/rest.service";
import { TrackDto } from "../../models/track.model";
import { CountryService } from "../../../../../core/services/country.service";
import { FlagUrlPipe } from "../../../../../shared/pipes/flag-url.pipe";

@Component({
    selector: 'track-list',
    templateUrl: './track-list.component.html',
    imports: [RouterLink, RouterOutlet, FlagUrlPipe],
})
export class TrackListComponent implements OnInit {
    restService = inject(RestService);
    countryService = inject(CountryService);
    tracks = signal<TrackDto[]>([]);

    ngOnInit(): void {
        this.restService.get<TrackDto[]>('/track/get-all').subscribe(res => this.tracks.set(res));
    }
}