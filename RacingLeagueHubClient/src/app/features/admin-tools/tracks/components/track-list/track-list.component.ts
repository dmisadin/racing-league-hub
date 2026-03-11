import { Component } from "@angular/core";
import { RouterLink, RouterOutlet } from "@angular/router";

@Component({
    selector: 'track-list',
    templateUrl: './track-list.component.html',
    imports: [RouterLink, RouterOutlet],
})
export class TrackListComponent {

}