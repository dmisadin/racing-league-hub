import { Component, inject, input, OnInit, signal } from "@angular/core";
import { TrackFormComponent } from "../track-form/track-form.component";
import { TrackLayoutFormComponent } from "../track-layout-form/track-layout-form.component";
import { RestService } from "../../../../../core/services/rest.service";
import { TrackDto } from "../../models/track.model";

@Component({
    selector: 'track-details',
    imports: [TrackFormComponent, TrackLayoutFormComponent],
    templateUrl: './track-details.component.html',
})
export class TrackDetailsComponent implements OnInit {
    private readonly restService = inject(RestService);

    trackId = input.required<string>();
    track = signal<TrackDto | null>(null);

    ngOnInit(): void {
        this.restService.get<TrackDto>('/track/get-by-id/' + this.trackId()).subscribe(res => {
            const edited = { ...res, trackLayouts: trackLayouts };
            this.track.set(edited)
            //this.track.set(res)
        });
    }

    addLayout() {
    }
}

const trackLayouts = [
    {
        id: 1,
        trackId: 1,
        name: "Grand Prix Layout",
        pitStopDuration: 25,
        cornersTotal: 16,
        cornersLeft: 7,
        lapsGrandPrix: 58,
        trackLayoutGames: [20, 21]
    },
    {
        id: 2,
        trackId: 1,
        name: "Short Circuit",
        pitStopDuration: null,
        cornersTotal: 10,
        cornersLeft: 4,
        lapsGrandPrix: 35,
        trackLayoutGames: [20, 21, 23]
    },
    {
        id: 3,
        trackId: 1,
        name: "Endurance Layout",
        pitStopDuration: 30,
        cornersTotal: 20,
        cornersLeft: 9,
        lapsGrandPrix: 70,
        trackLayoutGames: [19, 20, 21]
    }
];
