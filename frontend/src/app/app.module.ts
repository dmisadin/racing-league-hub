import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { ColorPickerModule } from 'ngx-color-picker';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { HomeComponent } from './features/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertComponent } from './shared/components/alert/alert.component';
import { AnnouncementCarouselComponent } from './shared/components/announcement-carousel/announcement-carousel.component';
import { BtnLargeComponent } from './shared/components/btn-large/btn-large.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { SidebarRaceComponent } from './shared/components/sidebar-race/sidebar-race.component';
import { LoginComponent } from './features/login/login.component';
import { RegistrationComponent } from './features/registration/registration.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { UtilityBarComponent } from './shared/components/utility-bar/utility-bar.component';

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

import { DriverComponent } from './features/driver/driver.component';

import { NotFoundComponent } from './features/not-found/not-found.component';
import { LeaguesListComponent } from './features/league/pages/leagues-list/leagues-list.component';
import { FooterComponent } from './shared/components/footer/footer.component';

import { SharedModule } from './shared/shared.module';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        HomeComponent,
        AlertComponent,
        AnnouncementCarouselComponent,
        BtnLargeComponent,
        SidebarComponent,
        SidebarRaceComponent,
        LoginComponent,
        RegistrationComponent,
        DriverComponent,
        NotFoundComponent,
        LeaguesListComponent,
        FooterComponent,
        UtilityBarComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        HttpClientModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        ColorPickerModule,
        SharedModule,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
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
