import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AccountSettingsComponent} from './pages/account-settings/account-settings.component';
import {HomeComponent} from './pages/home/home.component';
import {InstructorDetailsComponent} from './pages/instructors/instructor-details/instructor-details.component';
import {InstructorsComponent} from './pages/instructors/instructors.component';
import {LoginComponent} from './pages/login/login.component';
import {RegisterComponent} from './pages/register/register.component';
import {ReservationsComponent} from './pages/reservations/reservations.component';
import {WorkoutTypesComponent} from './pages/workout-types/workout-types.component';
import {WorkoutsComponent} from './pages/workouts/workouts.component';
import {AuthGuardService} from "./services/identity/auth-guard.service";
import {ReservationDetailsComponent} from "./pages/reservations/reservation-details/reservation-details.component";

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'workouts', component: WorkoutsComponent },
    { path: 'instructors', component: InstructorsComponent },
    { path: 'instructors/:id', component: InstructorDetailsComponent },
    { path: 'workout-types', component: WorkoutTypesComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'reservations', component: ReservationsComponent, canActivate: [AuthGuardService] },
    { path: 'reservations/:id', component: ReservationDetailsComponent, canActivate: [AuthGuardService] },
    { path: 'login', component: LoginComponent },
    {
      path: 'administration',
      loadChildren: () => import('./pages/administration/administration.module')
        .then((m) => m.AdministrationModule)
    },
    { path: 'account-settings', component: AccountSettingsComponent, canActivate: [AuthGuardService] },
    { path: '', redirectTo: '/home', pathMatch: 'prefix' }
  ];

  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}
