import {Component} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {RepetitiveWorkoutService} from "../../../services/repetitive-workout.service";
import {pageUrls} from "../../../../environments/page-urls";
import {environment} from "../../../../environments/environment";
import {Permission} from "../../../models/enums/permission.enum";

@Component({
  selector: 'app-repetitive-workouts',
  templateUrl: './repetitive-workouts.component.html',
  styleUrls: ['./repetitive-workouts.component.css']
})
export class RepetitiveWorkoutsComponent extends BaseComponent {
  isGeneratePopupVisible: boolean;
  isDeletePopupVisible: boolean;
  popupTitle: string;
  popupContent: string;
  permissions = Permission;

  constructor(private repetitiveWorkoutService: RepetitiveWorkoutService) {
    super();
    this.isGeneratePopupVisible = false;
    this.isDeletePopupVisible = false;
    this.popupTitle = "";
    this.popupContent = "";
  }

  forceGenerateUpcomingWeek() {
    this.subscribe(this.repetitiveWorkoutService.generateUpcomingWeek(), {
      next: () => this.notificationService.show("Workout generator has been launched. Check if the operation was successful!", "info"),
      error: () => this.notificationService.show("Forced generation of repetitive workouts failed", "error")
    });
  }

  deleteAllRepetitiveWorkouts() {
    this.subscribe(this.repetitiveWorkoutService.deleteAll(), {
      next: () => this.notificationService.show("Successfully removed all repetitive workouts ", "success"),
      error: () => this.notificationService.show("Failed to remove all repetitive workouts", "error")
    });
  }

  openHangfireDashboard = () => window.open(environment.serverUrl + pageUrls.hangfire, "Hangfire Dashboard");

  openDeletePopup = () => {
    this.popupTitle = "Delete all workouts";
    this.popupContent = "Permanent delete all repetitive workouts?";
    this.isDeletePopupVisible = true;
  }

  openGeneratePopup = () => {
    this.popupTitle = "Generate workouts for upcoming week";
    this.popupContent = "This action does not guarantee the execution of the generation. Check the hangfire dashboard to make sure the operation was successful.";
    this.isGeneratePopupVisible = true;
  }
}
