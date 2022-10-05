import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';
import { PagedQuery } from 'src/app/models/paged-query.model';
import { PagedResult } from 'src/app/models/paged-result.model';
import { WorkoutType } from 'src/app/models/workout-types.model';
import { WorkoutTypeService } from 'src/app/services/workout-type.service';

@Component({
  selector: 'app-workout-types',
  templateUrl: './workout-types.component.html',
  styleUrls: ['./workout-types.component.css']
})
export class WorkoutTypesComponent extends BaseComponent implements OnInit {
  workoutTypes?: PagedResult<WorkoutType>;
  query: PagedQuery;

  constructor(private workoutTypeService: WorkoutTypeService) {
    super();
    this.workoutTypes = new PagedResult<WorkoutType>();
    this.query = PagedQuery.default();
    this.query.sortBy = 'Name';
  }

  ngOnInit(): void {
    this.loadWorkoutTypes(this.query);
  }

  loadWorkoutTypes(params: PagedQuery): void {
    this.workoutTypeService.getAll(params).subscribe(
      (workoutTypes) => {
        this.workoutTypes = workoutTypes;
      }
    );
  }
}
