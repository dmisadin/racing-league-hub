import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SectionFlexComponent } from './components/section-flex/section-flex.component';
import { BtnCustomComponent } from './components/btn-custom/btn-custom.component';
import { SeasonCardComponent } from './components/season-card/season-card.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


@NgModule({
  declarations: [
    SectionFlexComponent,
    BtnCustomComponent,
    SeasonCardComponent,
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
  ],
  exports: [
    SectionFlexComponent,
    BtnCustomComponent,
    SeasonCardComponent,
  ]
})
export class SharedModule { }
