import {Injector, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import {AppComponent} from './app.component';
import {FooterComponent} from './components/footer/footer.component';
import {HeaderComponent} from './components/header/header.component';
import {HomeComponent} from './pages/home/home.component';
import {InstructorsComponent} from './pages/instructors/instructors.component';
import {WorkoutTypesComponent} from './pages/workout-types/workout-types.component';
import {AppRoutingModule} from './app-routing.module';
import {WorkoutsComponent} from './pages/workouts/workouts.component';
import {LoginComponent} from './pages/login/login.component';
import {RegisterComponent} from './pages/register/register.component';
import {InstructorDetailsComponent} from './pages/instructors/instructor-details/instructor-details.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {
  DxAccordionModule,
  DxButtonModule,
  DxDataGridModule,
  DxFormModule,
  DxListModule,
  DxLoadPanelModule,
  DxMenuModule,
  DxPopupModule,
  DxSchedulerModule,
  DxTagBoxModule,
  DxTextBoxModule,
  DxValidationGroupModule,
  DxValidationSummaryModule,
  DxValidatorModule
} from 'devextreme-angular';
import {DxiItemModule, DxoNotificationsModule} from 'devextreme-angular/ui/nested';
import {DatePipe} from '@angular/common';
import {ReservationsComponent} from './pages/reservations/reservations.component';
import {AccountSettingsComponent} from './pages/account-settings/account-settings.component';
import {NavBarComponent} from './components/header/nav-bar/nav-bar.component';
import {AdministrationModule} from "./pages/administration/administration.module";
import {ConfirmationPopupModule} from "./components/confirmation-popup/confirmation-popup.module";
import {PagerModule} from "./components/pager/pager.module";
import {TokenAuthorizationInterceptor} from "./interceptors/token-authorization.interceptor";
import {LogoComponent} from './components/header/logo/logo.component';
import {ReservationDetailsComponent} from './pages/reservations/reservation-details/reservation-details.component';
import {SearchPanelModule} from "./components/search-panel/search-panel.module";

export let AppInjector: Injector;

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    HomeComponent,
    InstructorsComponent,
    WorkoutTypesComponent,
    WorkoutsComponent,
    LoginComponent,
    RegisterComponent,
    InstructorDetailsComponent,
    ReservationsComponent,
    AccountSettingsComponent,
    NavBarComponent,
    LogoComponent,
    ReservationDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AdministrationModule,
    DxButtonModule,
    DxFormModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxiItemModule,
    DxMenuModule,
    DxValidationSummaryModule,
    DxValidationGroupModule,
    DxPopupModule,
    DxListModule,
    DxAccordionModule,
    DxSchedulerModule,
    DxoNotificationsModule,
    DxDataGridModule,
    DxLoadPanelModule,
    DxTagBoxModule,
    ConfirmationPopupModule,
    PagerModule,
    SearchPanelModule
  ],
  providers: [
    DatePipe,
      {
        provide: HTTP_INTERCEPTORS,
        useClass: TokenAuthorizationInterceptor,
        multi: true
      }
    ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private injector: Injector) {
    AppInjector = this.injector;
  }
}
