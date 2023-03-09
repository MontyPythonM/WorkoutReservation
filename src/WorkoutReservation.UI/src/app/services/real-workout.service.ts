import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {RealWorkout} from '../models/real-workout.model';
import {BaseService} from "../common/base.service";
import {apiUrl} from "../../environments/api-urls";
import {RealWorkoutCommand} from "../models/real-workout-command.model";
import {DATEONLY_FORMAT, TIMEONLY_FORMAT} from "../constants/constants";
import {DatePipe} from "@angular/common";

@Injectable({
  providedIn: 'root'
})
export class RealWorkoutService extends BaseService {

  constructor(protected override http: HttpClient, private datePipe: DatePipe) {
    super(http);
  }

  getRealWorkouts(): Observable<RealWorkout[]> {
    return super.get<RealWorkout[]>(apiUrl.realWorkout);
  }

  update(realWorkout: RealWorkoutCommand): Observable<void> {
    realWorkout.date = this.toDateOnly(realWorkout.date)
    realWorkout.startTime = this.toTimeOnly(realWorkout.startTime)
    realWorkout.endTime = this.toTimeOnly(realWorkout.endTime)
    return super.patch(apiUrl.realWorkout, { ...realWorkout });
  }

  remove(id: number): Observable<void> {
    return super.delete(apiUrl.realWorkout + id);
  }

  private toDateOnly = (dateTime: Date | string): string => this.datePipe.transform(dateTime, DATEONLY_FORMAT)!;
  private toTimeOnly = (dateTime: Date | string): string => this.datePipe.transform(dateTime, TIMEONLY_FORMAT)!;
}
