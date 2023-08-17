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


@NgModule({
    declarations: [
        SectionFlexComponent,
        BtnCustomComponent,
        SeasonCardComponent,
        IconLabelComponent,
        FormAlertComponent,
        SwitchComponent,
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
    ]
})
export class SharedModule { }
