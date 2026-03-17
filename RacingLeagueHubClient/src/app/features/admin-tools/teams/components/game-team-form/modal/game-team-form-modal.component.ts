import { Component, inject } from "@angular/core";
import { ModalComponent } from "../../../../../../shared/components/modal/modal.component";
import { RouteService } from "../../../../../../core/services/route.service";
import { GameTeamFormComponent } from "../game-team-form.component";

@Component({
    selector: 'track-add-modal',
    imports: [ModalComponent, GameTeamFormComponent],
    providers: [RouteService],
    template: `
        <modal [openByDefault]="true" 
            (onClose)="onModalClosed()" 
            (onDiscard)="onModalDiscarded()"
            title="Add new game specific team">
            <game-team-form (cancel)="onModalDiscarded()"></game-team-form>
        </modal>
    `,
})
export class GameTeamFormModalComponent {
    private readonly routeService = inject(RouteService);

    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}