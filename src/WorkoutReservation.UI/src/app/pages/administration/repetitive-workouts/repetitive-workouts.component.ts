import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {RepetitiveWorkoutService} from "../../../services/repetitive-workout.service";
import {NotificationService} from "../../../services/notification.service";
import {PageUrls} from "../../../../environments/page-urls";
import {environment} from "../../../../environments/environment";

@Component({
  selector: 'app-repetitive-workouts',
  templateUrl: './repetitive-workouts.component.html',
  styleUrls: ['./repetitive-workouts.component.css']
})
export class RepetitiveWorkoutsComponent extends BaseComponent implements OnInit {

  constructor(private repetitiveWorkoutService: RepetitiveWorkoutService,
              private notificationService: NotificationService) {
    super();
  }

  ngOnInit(): void {
  }

  forceGenerateUpcomingWeek(e: any) {
    this.subscribe(this.repetitiveWorkoutService.generateUpcomingWeek(), {
      next: () => this.notificationService.show("Forced generation of repetitive workouts successful", "success"),
      error: () => this.notificationService.show("Forced generation of repetitive workouts failed", "error")
    });
  }

  openHangfireDashboard(e: any) {
    window.open(environment.serverUrl + PageUrls.hangfire, "Hangfire Dashboard")
  }

  deleteAllRepetitiveWorkouts(e: any) {
    this.subscribe(this.repetitiveWorkoutService.deleteAll(), {
      next: () => this.notificationService.show("Successfully removed all repetitive workouts ", "success"),
      error: () => this.notificationService.show("Failed to remove all repetitive workouts", "error")
    })
  }
}
