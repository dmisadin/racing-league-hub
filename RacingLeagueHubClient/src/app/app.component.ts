import { Component, inject, model } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, RouterLinkActive } from '@angular/router';
import { ThemeService } from './core/services/theme.service';
import { FormsModule } from "@angular/forms";
import { ToastContainerComponent } from "./shared/components/toast/toast.component";
import { UserDropdownComponent } from "./features/account/components/user-dropdown/user-dropdown.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, FormsModule, RouterLinkWithHref, ToastContainerComponent, UserDropdownComponent, RouterLinkActive],
    templateUrl: './app.component.html'
})
export class App {
    private readonly themeService = inject(ThemeService);
    isLightMode = model(false);

    toggleTheme() {
        this.themeService.toggleTheme();
    }
}
