import { Component, inject, model } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { ThemeService } from './core/services/theme.service';
import { FormsModule } from "@angular/forms";
import { ToastContainerComponent } from "./shared/components/toast/toast.component";
import { UserDropdownComponent } from "./features/user/user-dropdown/user-dropdown.component";
import { IsAdminDirective } from "./core/directives/is-admin.directive";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, FormsModule, RouterLinkWithHref, ToastContainerComponent, UserDropdownComponent, IsAdminDirective],
    templateUrl: './app.component.html'
})
export class App {
    private readonly themeService = inject(ThemeService);
    isLightMode = model(false);

    toggleTheme() {
        this.themeService.toggleTheme();
    }
}
