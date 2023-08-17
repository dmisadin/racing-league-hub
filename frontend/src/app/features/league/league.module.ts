import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



import { LeaguePageComponent } from './pages/league-page/league-page.component';
import { LeagueAddEditComponent } from './components/league-add-edit/league-add-edit.component';
import { LeagueInfoComponent } from './components/league-info/league-info.component';
import { LeagueRoutingModule } from './league-routing.module';
//Shared components
import { SharedModule } from 'app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ColorPickerModule } from 'ngx-color-picker';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
    declarations: [
        LeaguePageComponent,
        LeagueAddEditComponent,
        LeagueInfoComponent,
    ],
    imports: [
        CommonModule,
        LeagueRoutingModule,
        SharedModule,
        ReactiveFormsModule,
        ColorPickerModule,
        FontAwesomeModule,
    ]
})
export class LeagueModule { }
