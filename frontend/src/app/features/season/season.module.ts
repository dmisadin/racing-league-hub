import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SeasonPageComponent } from './pages/season-page/season-page.component';
import { StandingsTablesComponent } from './components/season-tables/standings-tables/standings-tables.component';
import { SeasonFormsComponent } from './components/season-forms/season-forms.component';
import { SeasonInfoFormComponent } from './components/season-forms/season-info-form/season-info-form.component';
import { SeasonPointsComponent } from './components/season-forms/season-points/season-points.component';
import { SeasonLobbySettingsComponent } from './components/season-forms/season-lobby-settings/season-lobby-settings.component';
import { SeasonAssistsComponent } from './components/season-forms/season-assists/season-assists.component';
import { PointsItemComponent } from './components/season-forms/points-item/points-item.component';

//Shared modules and components
import { SharedModule } from 'app/shared/shared.module';
import { RaceCardComponent } from './components/race-card/race-card.component';
import { SeasonRoutingModule } from './season-routing.module';


@NgModule({
    declarations: [
        SeasonPageComponent,
        StandingsTablesComponent,
        SeasonFormsComponent,
        SeasonInfoFormComponent,
        SeasonPointsComponent,
        SeasonLobbySettingsComponent,
        SeasonAssistsComponent,
        PointsItemComponent,
        RaceCardComponent
    ],
    imports: [
        CommonModule,
        SeasonRoutingModule,
        SharedModule,
        ReactiveFormsModule,
        FontAwesomeModule,
    ]
})
export class SeasonModule { }
