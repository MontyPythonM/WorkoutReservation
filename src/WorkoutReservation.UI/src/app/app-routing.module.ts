import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { InstructorsComponent } from './pages/instructors/instructors.component';
import { WorkoutTypesComponent } from './pages/workout-types/workout-types.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'instructors', component: InstructorsComponent },
    { path: 'workout-types', component: WorkoutTypesComponent },    
    { path: '', redirectTo: '/home', pathMatch: 'prefix' }
  ];

  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}