import { Component } from "@angular/core";

@Component({
    selector: 'home',
    template: `
        <div class="space-y-4">
            @for(item of [].constructor(50); track $index)
            {
            <p>
                Scrollable content...
            </p>
            }
        </div>`,
})
export class HomeComponent {

}
