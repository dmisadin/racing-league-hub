import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SeasonPageComponent } from './pages/season-page/season-page.component';
import { SeasonFormsComponent } from './components/season-forms/season-forms.component';


const routes: Routes = [
    {
        path: 'add',
        component: SeasonFormsComponent
    },
    {
        path: ':id',
        title: "Season",
        component: SeasonPageComponent
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule],
    providers: []
})
export class SeasonRoutingModule {

}