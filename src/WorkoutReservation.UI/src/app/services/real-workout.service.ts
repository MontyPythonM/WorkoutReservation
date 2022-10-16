import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RealWorkout } from '../models/real-workout.model';

@Injectable({
  providedIn: 'root'
})
export class RealWorkoutService {

  constructor(private http: HttpClient) { }

  getCurrentWorkouts() {
    return this.http.get<RealWorkout>(environment.apiUrl + 'real-workout/current-week');
  }

  getUpcomingWorkouts() {
    return this.http.get<RealWorkout>(environment.apiUrl + 'real-workout/upcoming-week');
  }
}
