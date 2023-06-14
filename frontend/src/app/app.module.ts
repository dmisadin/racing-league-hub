import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from '../shared/components/navbar/navbar.component';
import { HomeComponent } from './features/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { AnnouncementCarouselComponent } from './announcement-carousel/announcement-carousel.component';
import { BtnLargeComponent } from '../shared/components/btn-large/btn-large.component';
import { SectionFlexComponent } from '../shared/components/section-flex/section-flex.component';
import { ResultRowComponent } from '../shared/components/result-row/result-row.component';
import { BtnCustomComponent } from '../shared/components/btn-custom/btn-custom.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    AlertComponent,
    AnnouncementCarouselComponent,
    BtnLargeComponent,
    SectionFlexComponent,
    ResultRowComponent,
    BtnCustomComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
