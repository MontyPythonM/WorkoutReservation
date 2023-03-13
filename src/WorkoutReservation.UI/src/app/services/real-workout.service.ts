import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {RealWorkout} from '../models/real-workout.model';
import {BaseService} from "../common/base.service";
import {apiUrl} from "../../environments/api-urls";
import {RealWorkoutCommand} from "../models/real-workout-command.model";

@Injectable({
  providedIn: 'root'
})
export class RealWorkoutService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getRealWorkouts(): Observable<RealWorkout[]> {
    return super.get<RealWorkout[]>(apiUrl.realWorkout);
  }

  create(realWorkout: RealWorkoutCommand): Observable<void> {
    return super.post(apiUrl.realWorkout, { ...realWorkout });
  }

  update(realWorkout: RealWorkoutCommand): Observable<void> {
    return super.patch(apiUrl.realWorkout, { ...realWorkout });
  }

  remove(id: number): Observable<void> {
    return super.delete(apiUrl.realWorkout + id);
  }
}
