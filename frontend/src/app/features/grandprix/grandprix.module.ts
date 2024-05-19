import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { GrandPrixComponent } from './pages/grandprix.component';
import { GrandPrixFormsComponent } from './components/grandprix-forms/grandprix-forms.component';
import { GrandprixInfoItemComponent } from './components/grandprix-forms/grandprix-info-item/grandprix-info-item.component';
import { TableQualifyingResultComponent } from './components/grandprix-tables/table-qualifying-result/table-qualifying-result.component';
import { TableRaceResultComponent } from './components/grandprix-tables/table-race-result/table-race-result.component';
import { GrandprixRoutingModule } from './grandprix-routing.module';

import { SharedModule } from 'app/shared/shared.module';

import { RaceTimePipe } from 'app/shared/pipes/race-time.pipe';
import { GrandprixRaceResultFormComponent } from './components/grandprix-forms/grandprix-race-result-form/grandprix-race-result-form.component';



@NgModule({
    declarations: [
        GrandPrixComponent,
        GrandPrixFormsComponent,
        GrandprixInfoItemComponent,
        TableQualifyingResultComponent,
        TableRaceResultComponent,

        RaceTimePipe,
        GrandprixRaceResultFormComponent,
    ],
    imports: [
        CommonModule,
        GrandprixRoutingModule,
        SharedModule,
        ReactiveFormsModule,

    ]
})
export class GrandprixModule { }
