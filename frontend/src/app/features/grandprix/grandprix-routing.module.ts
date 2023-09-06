import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { GrandPrixComponent } from "./pages/grandprix.component";
import { GrandPrixFormsComponent } from "./components/grandprix-forms/grandprix-forms.component";


const routes: Routes = [
    {
        path: "add",
        component: GrandPrixFormsComponent,
    },
    {
        path: ":grandPrixId",
        component: GrandPrixComponent,
    },
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule,
    ],
    providers: [

    ]
})
export class GrandprixRoutingModule {
}