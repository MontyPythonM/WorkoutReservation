import {Injectable} from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {apiUrl} from "../../environments/api-urls";
import {WorkoutTypeTag} from "../models/workout-type-tag.model";
import {WorkoutTypeTagActive} from "../models/workout-type-tag-active.model";
import {WorkoutTypeTagCommand} from "../models/workout-type-tag-command.model";

@Injectable({
  providedIn: 'root'
})
export class WorkoutTypeTagService extends BaseService  {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getActive(): Observable<WorkoutTypeTagActive[]> {
    return super.get<WorkoutTypeTagActive[]>(apiUrl.workoutTypeTag.onlyActive);
  }

  getAll(): Observable<WorkoutTypeTag[]> {
    return super.get<WorkoutTypeTag[]>(apiUrl.workoutTypeTag.baseUrl);
  }

  remove(id: number): Observable<void> {
    return super.delete<void>(apiUrl.workoutTypeTag.baseUrl + id);
  }

  create(tag: string): Observable<void> {
    return super.post<void>(apiUrl.workoutTypeTag.baseUrl, { tag });
  }

  update(workoutTypeTagCommand: WorkoutTypeTagCommand): Observable<void> {
    return super.put<void>(apiUrl.workoutTypeTag.baseUrl, { ...workoutTypeTagCommand });
  }
}
