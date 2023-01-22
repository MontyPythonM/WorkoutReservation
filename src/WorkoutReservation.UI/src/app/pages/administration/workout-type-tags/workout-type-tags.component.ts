import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {WorkoutTypeTag} from "../../../models/workout-type-tag.model";
import {WorkoutTypeTagService} from "../../../services/workout-type-tag.service";

@Component({
  selector: 'app-workout-type-tags',
  templateUrl: './workout-type-tags.component.html',
  styleUrls: ['./workout-type-tags.component.css']
})
export class WorkoutTypeTagsComponent extends BaseComponent implements OnInit {

  workoutTypeTags: WorkoutTypeTag[];

  constructor(private workoutTypeTagService: WorkoutTypeTagService) {
    super();
    this.workoutTypeTags = [];
  }

  ngOnInit(): void {
    this.loadWorkoutTypeTags();
  }

  protected loadWorkoutTypeTags(): void {
    this.subscribe(this.workoutTypeTagService.getAllWorkoutTypeTags(), {
      next: (response: WorkoutTypeTag[]) => this.workoutTypeTags = response
    })
  }

}
