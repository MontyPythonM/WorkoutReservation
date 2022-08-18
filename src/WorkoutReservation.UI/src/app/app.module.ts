import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { GalleryComponent } from './components/gallery/gallery.component';
import { HomeComponent } from './pages/home/home.component';
import { InstructorsComponent } from './pages/instructors/instructors.component';
import { WorkoutTypesComponent } from './pages/workout-types/workout-types.component';
import { AppRoutingModule } from './app-routing.module';
import { WorkoutsComponent } from './pages/workouts/workouts.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { InstructorDetailsComponent } from './pages/instructors/instructor-details/instructor-details.component';
import { WorkoutDetailsComponent } from './pages/workouts/workout-details/workout-details.component';
import { WorkoutTypeDetailsComponent } from './pages/workout-types/workout-type-details/workout-type-details.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatSliderModule } from '@angular/material/slider';
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatIconModule } from '@angular/material/icon'; 
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    GalleryComponent,
    HomeComponent,
    InstructorsComponent,
    WorkoutTypesComponent,
    WorkoutsComponent,
    LoginComponent,
    RegisterComponent,
    InstructorDetailsComponent,
    WorkoutDetailsComponent,
    WorkoutTypeDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,    
    HttpClientModule, 
    BrowserAnimationsModule,
    MatSliderModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
