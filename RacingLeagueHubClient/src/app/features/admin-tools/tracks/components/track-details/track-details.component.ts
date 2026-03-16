import { Component, inject, OnInit, signal } from "@angular/core";
import { TrackFormComponent } from "../track-form/track-form.component";
import { TrackLayoutFormComponent } from "../track-layout-form/track-layout-form.component";
import { RestService } from "../../../../../core/services/rest.service";
import { TrackDto } from "../../models/track.model";
import { RouterOutlet, RouterLinkWithHref } from "@angular/router";
import { RouteService } from "../../../../../core/services/route.service";

@Component({
    selector: 'track-details',
    imports: [TrackFormComponent, TrackLayoutFormComponent, RouterOutlet, RouterLinkWithHref],
    providers: [RouteService],
    templateUrl: './track-details.component.html',
})
export class TrackDetailsComponent implements OnInit {
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);

    trackId = signal<string | null>(null);
    track = signal<TrackDto | null>(null);

    ngOnInit(): void {
        const trackId = this.routeService.getRouteParam('trackId');
        console.log("ngOnInit", trackId)
        this.trackId.set(trackId);

        this.restService.get<TrackDto>('/track/get-by-id/' + this.trackId()).subscribe(res => {
            this.track.set(res)
        });
    }

    addLayout() {
    }
}