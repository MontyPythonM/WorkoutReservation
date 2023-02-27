import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {BaseComponent} from 'src/app/common/base.component';
import {DATE_FORMAT, TIME_FORMAT} from 'src/app/common/constants';
import {RealWorkout, RealWorkoutSchedule} from 'src/app/models/real-workout.model';
import {RealWorkoutService} from 'src/app/services/real-workout.service';
import {ReservationService} from "../../services/reservation.service";
import {pageUrls} from "../../../environments/page-urls";
import dxScheduler from "devextreme/ui/scheduler";

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrls: ['./workouts.component.css']
})
export class WorkoutsComponent extends BaseComponent implements OnInit {
  workouts: RealWorkoutSchedule[];
  dateFormat: string;
  timeFormat: string;
  schedulerInit?: dxScheduler;

  constructor(private realWorkoutService: RealWorkoutService,
              private router: Router,
              private reservationService: ReservationService) {
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

  bookWorkout(workoutId: number) {
    this.subscribe(this.reservationService.addReservation(workoutId), {
      next: () => {
        this.ngOnInit();
        this.notificationService.show("Workout successfully reserved", "success")
      },
      error: () => this.notificationService.show("Workout cannot be reserved", "error")
    })
  }

  routeToReservation = (reservationId: number)=> this.router.navigate([pageUrls.reservations + '/', reservationId]);

  onAppointmentFormOpening(e: any): void {
    const form = e.form;
    form.cancel = "true";
  }

  schedulerInitialize = (e: { component: dxScheduler }) => this.schedulerInit = e.component;
}
