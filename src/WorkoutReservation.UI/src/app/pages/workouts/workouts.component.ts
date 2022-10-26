import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';
import { RealWorkout } from 'src/app/models/real-workout.model';
import { RealWorkoutService } from 'src/app/services/real-workout.service';

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrls: ['./workouts.component.css']
})
export class WorkoutsComponent extends BaseComponent implements OnInit {

  currentWeekWorkouts?: RealWorkout;
  upcomingWeekWorkouts?: RealWorkout;


  constructor(private realWorkoutService: RealWorkoutService) {
    super();
  }

  ngOnInit(): void {
    this.loadCurrentWeekWorkouts();
    this.loadUpcomingWeekWorkouts();
    console.log("currentWeekWorkouts: ", this.currentWeekWorkouts);
    console.log("upcomingWeekWorkouts: ", this.upcomingWeekWorkouts);
  }

  loadCurrentWeekWorkouts(): void {
    this.subscribe(this.realWorkoutService.getCurrentWorkouts(), {
      next: (response: RealWorkout) => {
        this.currentWeekWorkouts = response;
      }
    });
  }

  loadUpcomingWeekWorkouts(): void {
    this.subscribe(this.realWorkoutService.getUpcomingWorkouts(), {
      next: (response: RealWorkout) => {
        this.upcomingWeekWorkouts = response;
      }
    });
  }
}
