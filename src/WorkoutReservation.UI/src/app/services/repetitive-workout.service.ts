import {Injectable} from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {apiUrl} from "../../environments/api-urls";
import {RepetitiveWorkout} from "../models/repetitive-workout.model";
import {RealWorkoutCommand} from "../models/real-workout-command.model";
import {RepetitiveWorkoutCommand} from "../models/repetitive-workout-command.model";

@Injectable({
  providedIn: 'root'
})
export class RepetitiveWorkoutService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  generateUpcomingWeek(): Observable<void> {
    return super.post<void>(apiUrl.repetitiveWorkout.generateUpcomingWeek);
  }

  deleteAll(): Observable<void> {
    return super.delete<void>(apiUrl.repetitiveWorkout.deleteAll);
  }

  getRepetitiveWorkouts(): Observable<RepetitiveWorkout[]> {
    return super.get<RepetitiveWorkout[]>(apiUrl.repetitiveWorkout.baseUrl)
  }

  create(repetitiveWorkout: RepetitiveWorkoutCommand): Observable<void> {
    return super.post(apiUrl.repetitiveWorkout.baseUrl, { ...repetitiveWorkout });
  }

  update(repetitiveWorkout: RepetitiveWorkoutCommand): Observable<void> {
    return super.put(apiUrl.repetitiveWorkout.baseUrl, { ...repetitiveWorkout });
  }

  remove(id: number): Observable<void> {
    return super.delete(apiUrl.repetitiveWorkout.baseUrl + id)
  }
}
