import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginBtnComponent } from './login-btn/login-btn.component';
import { MainContentComponent } from './main-content/main-content.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { AlertComponent } from './alert/alert.component';
import { AnnouncementCarouselComponent } from './announcement-carousel/announcement-carousel.component';
import { MainButtonsComponent } from './main-buttons/main-buttons.component';
import { MainBtnComponent } from './main-btn/main-btn.component';
import { MainResultsComponent } from './main-results/main-results.component';
import { MainResultComponent } from './main-result/main-result.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginBtnComponent,
    MainContentComponent,
    AlertComponent,
    AnnouncementCarouselComponent,
    MainButtonsComponent,
    MainBtnComponent,
    MainResultsComponent,
    MainResultComponent
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
