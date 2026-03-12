import { Component, inject } from "@angular/core";
import { ModalComponent } from "../../../../../shared/components/modal/modal.component";
import { TrackFormComponent } from "../track-form/track-form.component";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
    selector: 'track-add-modal',
    imports: [ModalComponent, TrackFormComponent],
    template: `
        <modal [openByDefault]="true" (onClose)="onModalClosed()" (onDiscard)="onModalDiscarded()" title="Add new track">
            <track-form (cancel)="onModalDiscarded()"></track-form>
        </modal>
    `,
})
export class TrackAddModalComponent {
    router = inject(Router);
    route = inject(ActivatedRoute);
    
    onModalClosed() {
        this.router.navigate(['../'], { relativeTo: this.route });
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}