import { Component, inject, model } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { ThemeService } from './core/services/theme.service';
import { FormsModule } from "@angular/forms";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, FormsModule, RouterLinkWithHref],
    templateUrl: './app.component.html'
})
export class App {
    private readonly themeService = inject(ThemeService);
    isLightMode = model(false);

    toggleTheme() {
        this.themeService.toggleTheme();
    }
}
