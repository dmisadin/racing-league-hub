import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { LeaguePageComponent } from './pages/league-page/league-page.component';
import { LeagueAddEditComponent } from './components/league-add-edit/league-add-edit.component';
import { LeagueInfoComponent } from './components/league-info/league-info.component';
import { LeagueRoutingModule } from './league-routing.module';
//Shared components
import { SharedModule } from 'app/shared/shared.module';
import { ColorPickerModule } from 'ngx-color-picker';

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
