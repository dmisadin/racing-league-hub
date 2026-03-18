import { Component, inject, OnInit, signal } from "@angular/core";
import { TrackFormComponent } from "../track-form/track-form.component";
import { TrackLayoutFormComponent } from "../track-layout-form/track-layout-form.component";
import { TrackDto } from "../../models/track.model";
import { RouterOutlet, RouterLinkWithHref } from "@angular/router";
import { RouteService } from "../../../../../core/services/route.service";
import { ModalFormParent } from "../../../../../shared/components/modal/modal-form-parent";

@Component({
    selector: 'track-details',
    imports: [TrackFormComponent, TrackLayoutFormComponent, RouterOutlet, RouterLinkWithHref],
    providers: [RouteService],
    templateUrl: './track-details.component.html',
})
export class TrackDetailsComponent extends ModalFormParent<TrackDto> implements OnInit {
    private readonly routeService = inject(RouteService);

    trackId = signal<string | null>(null);

    ngOnInit(): void {
        const trackId = this.routeService.getRouteParam('trackId');
        this.trackId.set(trackId);
    }
    
    protected override loadDto(): void {
        this.restService.get<TrackDto>('/track/get-by-id/' + this.trackId()).subscribe(res => {
            this.dto.set(res)
        });
    }
}
