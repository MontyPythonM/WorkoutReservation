import { Injectable } from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApiUrl} from "../../environments/api-urls";

@Injectable({
  providedIn: 'root'
})
export class RepetitiveWorkoutService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  generateUpcomingWeek(): Observable<void> {
    return super.post<void>(ApiUrl.repetitiveWorkout.generateUpcomingWeek);
  }

  deleteAll(): Observable<void> {
    return super.delete<void>(ApiUrl.repetitiveWorkout.deleteAll);
  }
}
