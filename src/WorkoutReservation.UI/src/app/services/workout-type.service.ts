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
  paginatedResult: PagedResult<WorkoutType>;

  constructor(private http: HttpClient) {
    this.paginatedResult = new PagedResult<WorkoutType>();
  }

  getAll(pageNumber: number, pageSize: number, sortByDescending: boolean): Observable<PagedResult<WorkoutType>> {
    const query = {
      PageNumber: pageNumber,
      PageSize: pageSize,
      SortByDescending: sortByDescending
    };

    return this.http.get<PagedResult<WorkoutType>>(environment.apiUrl + 'workout-type',
    { params: query }).pipe(
      map((response) => {
        response.items = response.items.map((workoutType) => new WorkoutType(workoutType));
        return response;
      })
    )
  }
}