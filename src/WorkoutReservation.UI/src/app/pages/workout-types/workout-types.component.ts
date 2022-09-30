import { Component, OnInit } from '@angular/core';
import { PagedResult } from 'src/app/models/paged-result.model';
import { WorkoutType } from 'src/app/models/workout-types.model';
import { WorkoutTypeService } from 'src/app/services/workout-type.service';

@Component({
  selector: 'app-workout-types',
  templateUrl: './workout-types.component.html',
  styleUrls: ['./workout-types.component.css']
})
export class WorkoutTypesComponent implements OnInit {
  workoutTypes?: PagedResult<WorkoutType>;

  constructor(private workoutTypeService: WorkoutTypeService) {
    this.workoutTypes = new PagedResult<WorkoutType>();
  }

  ngOnInit(): void {
    this.loadWorkoutTypes();
  }

  loadWorkoutTypes(): void {
    this.workoutTypeService.getAll(1, 5, true).subscribe(
      (workoutTypes) => {
        this.workoutTypes = workoutTypes;
        console.log('test');
      }
    );
  }
}
