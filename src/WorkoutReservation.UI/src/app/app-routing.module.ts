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
import {WorkoutDetailsComponent} from './pages/workouts/workout-details/workout-details.component';
import {WorkoutsComponent} from './pages/workouts/workouts.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'workouts', component: WorkoutsComponent },
    { path: 'workouts/:id', component: WorkoutDetailsComponent },
    { path: 'instructors', component: InstructorsComponent },
    { path: 'instructors/:id', component: InstructorDetailsComponent },
    { path: 'workout-types', component: WorkoutTypesComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'reservations', component: ReservationsComponent },
    { path: 'login', component: LoginComponent },
    {
      path: 'administration',
      loadChildren: () => import('./pages/administration/administration.module')
        .then((m) => m.AdministrationModule)
    },
    { path: 'account-settings', component: AccountSettingsComponent },
    { path: '', redirectTo: '/home', pathMatch: 'prefix' }
  ];

  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}
