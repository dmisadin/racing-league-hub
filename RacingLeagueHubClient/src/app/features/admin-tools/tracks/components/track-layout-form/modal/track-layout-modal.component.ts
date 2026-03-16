import { Component, inject } from "@angular/core";
import { ModalComponent } from "../../../../../../shared/components/modal/modal.component";
import { TrackLayoutFormComponent } from "../track-layout-form.component";
import { RouteService } from "../../../../../../core/services/route.service";

@Component({
    selector: 'track-add-modal',
    imports: [ModalComponent, TrackLayoutFormComponent],
    template: `
        <modal [openByDefault]="true" (onClose)="onModalClosed()" (onDiscard)="onModalDiscarded()" title="Add new track">
            <track-layout-form (cancel)="onModalDiscarded()"></track-layout-form>
        </modal>
    `,
})
export class TrackAddModalComponent {
    private readonly routeService = inject(RouteService);
    
    onModalClosed() {
        const trackId = this.routeService.getRouteParam("trackId") ?? "";
        this.routeService.navigateToRelative("../", trackId);
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}