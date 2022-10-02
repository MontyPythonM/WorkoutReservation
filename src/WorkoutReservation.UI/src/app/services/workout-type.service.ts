import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PagedResult } from '../models/paged-result.model';
import { WorkoutType } from '../models/workout-types.model';

@Injectable({
  providedIn: 'root'
})
export class WorkoutTypeService {

  constructor(private http: HttpClient) {}

  getAll(queryParams: any): Observable<PagedResult<WorkoutType>> {
    return this.http.get<PagedResult<WorkoutType>>(environment.apiUrl + 'workout-type',
    { params: queryParams }).pipe(
      map((response) => {
        response.items = response.items.map((workoutType) => new WorkoutType(workoutType));
        return response;
      })
    )
  }
}