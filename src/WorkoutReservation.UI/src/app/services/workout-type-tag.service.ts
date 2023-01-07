import {Injectable} from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {apiUrl} from "../../environments/api-urls";
import {WorkoutTypeTag} from "../models/workout-type-tag.model";

@Injectable({
  providedIn: 'root'
})
export class WorkoutTypeTagService extends BaseService  {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getActiveWorkoutTypeTags(): Observable<WorkoutTypeTag[]> {
    return super.get<WorkoutTypeTag[]>(apiUrl.workoutTypeTag.onlyActive);
  }

  getAllWorkoutTypeTags(): Observable<WorkoutTypeTag[]> {
    return super.get<WorkoutTypeTag[]>(apiUrl.workoutTypeTag.all);
  }

  deactivate(id: number): Observable<void> {
    return super.patch<void>(apiUrl.workoutTypeTag.deactivate + id);
  }

  create(tag: string): Observable<void> {
    return super.post<void>(apiUrl.workoutTypeTag.create, { tag });
  }
}
