import { Component, inject } from "@angular/core";
import { ModalComponent } from "../../../../../../shared/components/modal/modal.component";
import { TrackFormComponent } from "../track-form.component";
import { RouteService } from "../../../../../../core/services/route.service";

@Component({
    selector: 'track-add-modal',
    imports: [ModalComponent, TrackFormComponent],
    providers: [RouteService],
    template: `
        <modal [openByDefault]="true" (onClose)="onModalClosed()" (onDiscard)="onModalDiscarded()" title="Add new track">
            <track-form (cancel)="onModalDiscarded()"></track-form>
        </modal>
    `,
})
export class TrackAddModalComponent {
    private readonly routeService = inject(RouteService);
    
    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}