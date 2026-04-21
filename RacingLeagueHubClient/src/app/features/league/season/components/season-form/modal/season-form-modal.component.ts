import { Component, inject } from '@angular/core';
import { RouteService } from '../../../../../../core/services/route.service';
import { ModalComponent } from '../../../../../../shared/components/modal/modal.component';
import { SeasonFormComponent } from "../season-form.component";

@Component({
    selector: 'season-form-modal',
    imports: [ModalComponent, SeasonFormComponent],
    providers: [RouteService],
    template: `
          <modal [openByDefault]="true" 
              (onClose)="onModalClosed()" 
              (onDiscard)="onModalDiscarded()"
              title="Create a new Season">
              <season-form (cancel)="onModalDiscarded()"></season-form>
          </modal>
      `,
})
export class SeasonFormModalComponent {
    private readonly routeService = inject(RouteService);

    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}
