import { Component } from "@angular/core";
import { RouterOutlet, RouterLinkWithHref, RouterLinkActive } from "@angular/router";

@Component({
    selector: 'admin-tools',
    imports: [RouterOutlet, RouterLinkWithHref, RouterLinkActive],
    templateUrl: './admin-tools.component.html'
})
export class AdminToolsComponent {
    adminToolRoutes = [
        { path: "teams", label: "Teams" },
        { path: "tracks", label: "Tracks" }
    ]
}
