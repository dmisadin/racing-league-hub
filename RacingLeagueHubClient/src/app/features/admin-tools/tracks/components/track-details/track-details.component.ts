import { Component } from "@angular/core";
import { TrackFormComponent } from "../track-form/track-form.component";
import { TrackLayoutFormComponent } from "../track-layout-form/track-layout-form.component";

@Component({
    selector: 'track-details',
    imports: [TrackFormComponent, TrackLayoutFormComponent],
    templateUrl: './track-details.component.html',
})
export class TrackDetailsComponent {

}