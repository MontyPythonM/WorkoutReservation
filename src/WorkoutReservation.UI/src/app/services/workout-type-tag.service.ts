import { Injectable } from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApiUrl} from "../../environments/api-urls";
import {WorkoutTypeTag} from "../models/workout-type-tag.model";
import {WorkoutTypeCommand} from "../models/workout-types-command.model";

@Injectable({
  providedIn: 'root'
})
export class WorkoutTypeTagService extends BaseService  {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getActiveWorkoutTypeTags(): Observable<WorkoutTypeTag[]> {
    return super.get<WorkoutTypeTag[]>(ApiUrl.workoutTypeTag.onlyActive);
  }

  getAllWorkoutTypeTags(): Observable<WorkoutTypeTag[]> {
    return super.get<WorkoutTypeTag[]>(ApiUrl.workoutTypeTag.all);
  }

  deactivate(id: number): Observable<void> {
    return super.patch<void>(ApiUrl.workoutTypeTag.deactivate + id);
  }

  create(tag: string): Observable<void> {
    return super.post<void>(ApiUrl.workoutTypeTag.create, { tag });
  }
}
