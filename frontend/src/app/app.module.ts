import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DropdownModule } from '@coreui/angular';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { HomeComponent } from './features/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertComponent } from './shared/components/alert/alert.component';
import { AnnouncementCarouselComponent } from './shared/components/announcement-carousel/announcement-carousel.component';
import { BtnLargeComponent } from './shared/components/btn-large/btn-large.component';
import { SectionFlexComponent } from './shared/components/section-flex/section-flex.component';
import { RaceRowComponent } from './shared/components/race-row/race-row.component';
import { BtnCustomComponent } from './shared/components/btn-custom/btn-custom.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { SidebarRaceComponent } from './shared/components/sidebar-race/sidebar-race.component';
import { LoginComponent } from './features/login/login.component';
import { UserInfoComponent } from './shared/components/user-info/user-info.component';
import { RegistrationComponent } from './features/registration/registration.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { LeagueComponent } from './features/league/league.component';
import { SeasonCardComponent } from './shared/components/season-card/season-card.component';
import {
  FontAwesomeModule,
  FaIconLibrary,
} from '@fortawesome/angular-fontawesome';
import {
  faCircle,
  faCirclePlay,
  faCoffee,
  faPlus,
} from '@fortawesome/free-solid-svg-icons';
import { SeasonComponent } from './features/season/season.component';
import { IconLabelComponent } from './shared/components/icon-label/icon-label.component';
import { TableDriverStandingsComponent } from './shared/components/table-driver-standings/table-driver-standings.component';
import { TableTeamStandingsComponent } from './shared/components/table-team-standings/table-team-standings.component';
import { RaceCardComponent } from './shared/components/race-card/race-card.component';
import { GrandPrixComponent } from './features/grandprix/grandprix.component';
import { TableRaceResultComponent } from './shared/components/table-race-result/table-race-result.component';
import { TableQualifyingResultComponent } from './shared/components/table-qualifying-result/table-qualifying-result.component';
import { DriverComponent } from './features/driver/driver.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    AlertComponent,
    AnnouncementCarouselComponent,
    BtnLargeComponent,
    SectionFlexComponent,
    RaceRowComponent,
    BtnCustomComponent,
    SidebarComponent,
    SidebarRaceComponent,
    LoginComponent,
    RegistrationComponent,
    UserInfoComponent,
    LeagueComponent,
    SeasonCardComponent,
    SeasonComponent,
    IconLabelComponent,
    TableDriverStandingsComponent,
    TableTeamStandingsComponent,
    RaceCardComponent,
    GrandPrixComponent,
    TableRaceResultComponent,
    TableQualifyingResultComponent,
    DriverComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DropdownModule,
    FormsModule,
    HttpClientModule,
    FontAwesomeModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    // Add an icon to the library for convenient access in other components
    library.addIcons(faPlus, faCoffee, faCirclePlay, faCircle);
  }
}
