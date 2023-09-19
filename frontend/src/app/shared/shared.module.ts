import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SectionFlexComponent } from './components/section-flex/section-flex.component';
import { BtnCustomComponent } from './components/btn-custom/btn-custom.component';
import { SeasonCardComponent } from './components/season-card/season-card.component';
import { IconLabelComponent } from './components/icon-label/icon-label.component';
import { FormAlertComponent } from './components/form-alert/form-alert.component';
import { SwitchComponent } from './components/switch/switch.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RaceRowComponent } from './components/race-row/race-row.component';
import { UrlHttpPipe } from './pipes/url-http.pipe';
import { DateDiffFromNowPipe } from './pipes/date-diff-from-now.pipe';
import { NoSpacePipe } from './pipes/no-space.pipe';
import { UserInfoComponent } from './components/user-info/user-info.component';

@NgModule({
    declarations: [
        SectionFlexComponent,
        BtnCustomComponent,
        SeasonCardComponent,
        IconLabelComponent,
        FormAlertComponent,
        SwitchComponent,
        RaceRowComponent,
        UserInfoComponent,

        UrlHttpPipe,
        DateDiffFromNowPipe,
        NoSpacePipe,
    ],
    imports: [
        CommonModule,
        FontAwesomeModule,
        ReactiveFormsModule,
    ],
    exports: [
        SectionFlexComponent,
        BtnCustomComponent,
        SeasonCardComponent,
        IconLabelComponent,
        FormAlertComponent,
        SwitchComponent,
        RaceRowComponent,
        UserInfoComponent,
        
        UrlHttpPipe,
        DateDiffFromNowPipe,
        NoSpacePipe,
    ],
    providers: [
        DateDiffFromNowPipe,
    ]
})
export class SharedModule { }
