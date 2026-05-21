import { Directive, inject, TemplateRef, ViewContainerRef, effect } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

@Directive({
    selector: '[isAdmin]'
})
export class IsAdminDirective {
    private readonly authService = inject(AuthService);
    private readonly vcr = inject(ViewContainerRef);
    private readonly tpl = inject(TemplateRef);

    constructor() {
        effect(() => {
            if (this.authService.isAdmin()) {
                this.vcr.createEmbeddedView(this.tpl);
            } else {
                this.vcr.clear();
            }
        });
    }
}