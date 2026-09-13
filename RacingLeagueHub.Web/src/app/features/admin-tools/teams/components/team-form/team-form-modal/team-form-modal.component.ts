import { Component, inject } from "@angular/core";
import { ModalComponent } from "../../../../../../shared/components/modal/modal.component";
import { TeamFormComponent } from "../team-form.component";
import { RouteService } from "../../../../../../core/services/route.service";

@Component({
    selector: 'team-form-modal',
    imports: [ModalComponent, TeamFormComponent],
    providers: [RouteService],
    template: `
        <modal [openByDefault]="true" (onClose)="onModalClosed()" (onDiscard)="onModalDiscarded()" title="Add new team">
            <team-form (cancel)="onModalDiscarded()"></team-form>
        </modal>
    `,
})
export class TeamFormModalComponent {
    private readonly routeService = inject(RouteService);
    
    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}