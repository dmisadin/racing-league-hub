import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";

@Component({
    template: `
        <div class="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
            <div class="mx-auto max-w-screen-sm text-center">
                <h1 class="mb-4 text-7xl tracking-tight font-extrabold lg:text-9xl">404</h1>
                <p class="mb-4 text-3xl tracking-tight font-bold md:text-4xl">Something's missing.</p>
                <p class="mb-4 text-lg font-light text-gray-500">Sorry, we can't find that page. You'll find lots to explore on the home page. </p>
                <a routerLink="/" class="inline-flex bg-base-100 font-medium rounded-lg text-sm px-5 py-2.5 text-center my-4">Back to Homepage</a>
            </div>   
        </div>`,
    imports: [RouterLink]
})
export class NotFoundComponent {

}