import { Component, inject } from '@angular/core';
import { GrandPrixFormComponent } from "../grand-prix-form.component";
import { ModalComponent } from '../../../../../../../shared/components/modal/modal.component';
import { RouteService } from '../../../../../../../core/services/route.service';

@Component({
    selector: 'grand-prix-form-modal',
    imports: [ModalComponent, GrandPrixFormComponent],
    providers: [RouteService],
    template: `
          <modal [openByDefault]="true" 
              (onClose)="onModalClosed()" 
              (onDiscard)="onModalDiscarded()"
              title="Create a new Grand Prix">
              <grand-prix-form (cancel)="onModalDiscarded()"></grand-prix-form>
          </modal>
      `,
})
export class GrandPrixFormModalComponent {
    private readonly routeService = inject(RouteService);

    onModalClosed() {
        this.routeService.navigateToParent();
    }

    onModalDiscarded() {
        this.onModalClosed();
    }
}
