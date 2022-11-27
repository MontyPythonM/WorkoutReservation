import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RealWorkout } from '../models/real-workout.model';
import {BaseService} from "../common/base.service";
import {ApiUrl} from "../../environments/api-urls";

@Injectable({
  providedIn: 'root'
})
export class RealWorkoutService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getCurrentWorkouts(): Observable<RealWorkout[]> {
    return super.get<RealWorkout[]>(ApiUrl.realWorkout.current);
  }

  getUpcomingWorkouts(): Observable<RealWorkout[]> {
    return super.get<RealWorkout[]>(ApiUrl.realWorkout.upcoming);
  }
}
