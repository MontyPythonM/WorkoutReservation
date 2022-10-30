import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/common/base.component';
import { DATE_FORMAT, TIME_FORMAT } from 'src/app/common/constants';
import { RealWorkout, RealWorkoutSchedule } from 'src/app/models/real-workout.model';
import { RealWorkoutService } from 'src/app/services/real-workout.service';

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrls: ['./workouts.component.css']
})
export class WorkoutsComponent extends BaseComponent implements OnInit {
  workouts: RealWorkoutSchedule[];
  dateFormat: string;
  timeFormat: string;

  constructor(private realWorkoutService: RealWorkoutService, private router: Router) {
    super();
    this.workouts = [];
    this.dateFormat = DATE_FORMAT;
    this.timeFormat = TIME_FORMAT;
  }

  ngOnInit(): void {
    this.loadCurrentWeekWorkouts();
    this.loadUpcomingWeekWorkouts();
  }

  loadCurrentWeekWorkouts(): void {
    this.subscribe(this.realWorkoutService.getCurrentWorkouts(), {
      next: (response: RealWorkout[]) => {
        this.workouts.push(...response.map(rw => new RealWorkoutSchedule(rw)))
      }
    });
  }

  loadUpcomingWeekWorkouts(): void {
    this.subscribe(this.realWorkoutService.getUpcomingWorkouts(), {
      next: (response: RealWorkout[]) => {
        this.workouts.push(...response.map(rw => new RealWorkoutSchedule(rw)))
      }
    });
  }

  routeToReservationPage = (workoutId: number): Promise<boolean> => this.router.navigate(['/get-reservation']);
}
