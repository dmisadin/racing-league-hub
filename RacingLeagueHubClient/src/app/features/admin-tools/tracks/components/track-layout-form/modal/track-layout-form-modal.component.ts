import { Component, inject } from "@angular/core";
import { ModalComponent } from "../../../../../../shared/components/modal/modal.component";
import { TrackLayoutFormComponent } from "../track-layout-form.component";
import { RouteService } from "../../../../../../core/services/route.service";

@Component({
    selector: 'track-add-modal',
    imports: [ModalComponent, TrackLayoutFormComponent],
    providers: [RouteService],
    template: `
        <modal [openByDefault]="true" (onClose)="onModalClosed()" (onDiscard)="onModalDiscarded()" title="Add new track layout">
            <track-layout-form (cancel)="onModalDiscarded()"></track-layout-form>
        </modal>
    `,
})
export class TrackLayoutFormModalComponent {
    private readonly routeService = inject(RouteService);

    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}