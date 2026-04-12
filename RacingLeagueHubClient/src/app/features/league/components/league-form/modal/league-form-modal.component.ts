import { Component, inject } from '@angular/core';
import { ModalComponent } from "../../../../../shared/components/modal/modal.component";
import { RouteService } from '../../../../../core/services/route.service';
import { LeagueFormComponent } from "../league-form.component";

@Component({
    selector: 'league-form-modal',
    imports: [ModalComponent, LeagueFormComponent],
    providers: [RouteService],
    template: `
        <modal [openByDefault]="true" 
            (onClose)="onModalClosed()" 
            (onDiscard)="onModalDiscarded()"
            title="Create a new league">
            <league-form (cancel)="onModalDiscarded()"></league-form>
        </modal>
    `,
})
export class LeagueFormModalComponent {
    private readonly routeService = inject(RouteService);


    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}
