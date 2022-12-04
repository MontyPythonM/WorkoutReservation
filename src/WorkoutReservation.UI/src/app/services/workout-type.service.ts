import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable} from 'rxjs';
import {PagedResult} from '../models/paged-result.model';
import {WorkoutType} from '../models/workout-types.model';
import {ApiUrl} from "../../environments/api-urls";
import {BaseService} from "../common/base.service";
import {WorkoutTypeCommand} from "../models/workout-types-command.model";

@Injectable({
  providedIn: 'root'
})
export class WorkoutTypeService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getAll(queryParams: any): Observable<PagedResult<WorkoutType>> {
    return super.get<PagedResult<WorkoutType>>(ApiUrl.workoutType,
    { ...queryParams }).pipe(
      map((response) => {
        response.items = response.items.map((workoutType) => new WorkoutType(workoutType));
        return response;
      })
    )
  }

  remove(id: number): Observable<void> {
    return super.delete<void>(ApiUrl.workoutType + id);
  }

  create(workoutType: WorkoutTypeCommand): Observable<void> {
    return super.post(ApiUrl.workoutType, { ...workoutType });
  }

  update(workoutType: WorkoutTypeCommand): Observable<void> {
    return super.put(ApiUrl.workoutType, { ...workoutType });
  }
}
