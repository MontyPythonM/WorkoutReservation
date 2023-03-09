import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {BaseComponent} from 'src/app/common/base.component';
import {DATE_FORMAT, TIME_FORMAT} from 'src/app/common/constants';
import {RealWorkout} from 'src/app/models/real-workout.model';
import {RealWorkoutService} from 'src/app/services/real-workout.service';
import {ReservationService} from "../../services/reservation.service";
import {pageUrls} from "../../../environments/page-urls";
import dxScheduler from "devextreme/ui/scheduler";
import {Permission} from "../../models/enums/permission.enum";
import {Instructor} from "../../models/instructor.model";
import {InstructorService} from "../../services/instructor.service";
import dxForm from "devextreme/ui/form";
import {RealWorkoutCommand} from "../../models/real-workout-command.model";

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrls: ['./workouts.component.css']
})
export class WorkoutsComponent extends BaseComponent implements OnInit {
  workouts: RealWorkout[];
  dateFormat: string;
  timeFormat: string;
  schedulerInit?: dxScheduler;
  editFormInit?: dxForm;
  editPopupVisible: boolean;
  workoutDetails?: RealWorkout;
  realWorkoutCommand?: RealWorkoutCommand;
  permissions = Permission;
  isEditMode: boolean;
  addPopupVisible: boolean;
  instructors?: Instructor[];
  popupHeight: number;
  isDeletePopupVisible: boolean;

  constructor(private realWorkoutService: RealWorkoutService,
              private router: Router,
              private reservationService: ReservationService,
              private instructorService: InstructorService) {
    super();
    this.workouts = [];
    this.dateFormat = DATE_FORMAT;
    this.timeFormat = TIME_FORMAT;
    this.editPopupVisible = false;
    this.isEditMode = false;
    this.addPopupVisible = false;
    this.popupHeight = 345;
    this.isDeletePopupVisible = false;
  }

  ngOnInit(): void {
    this.loadWorkouts();
  }

  loadWorkouts(): void {
    this.subscribe(this.realWorkoutService.getRealWorkouts(), {
      next: (result: RealWorkout[]) => this.workouts = result
    });
  }

  loadInstructors() {
    this.subscribe(this.instructorService.getAll(), {
      next: (result: Instructor[]) => this.instructors = result
    });
  }

  bookWorkout(workoutId: number) {
    this.subscribe(this.reservationService.addReservation(workoutId), {
      next: () => {
        this.ngOnInit();
        this.notificationService.show("Workout successfully reserved", "success");
        this.closeEditPopup();
      },
      error: () => this.notificationService.show("Workout cannot be reserved", "error")
    })
  }

  addRealWorkout() {
    // TODO
  }

  editRealWorkout() {
    const isValid = this.editFormInit!.validate().isValid;
    if (isValid) {
      this.subscribe(this.realWorkoutService.update(this.realWorkoutCommand!), {
        next: () => {
          this.ngOnInit();
          this.notificationService.show("Real workout successfully updated", "success");
          this.closeEditPopup();
        }
      });
    }
  }

  deleteRealWorkout() {
    this.subscribe(this.realWorkoutService.remove(this.realWorkoutCommand?.id!), {
      next: () => {
        this.ngOnInit();
        this.editPopupVisible = false;
        this.notificationService.show("Workout successfully deleted", "success");
      }
    });
  }

  toggleEditMode = () => {
    this.popupHeight = 440;
    this.isEditMode = !this.isEditMode;
    if (this.isEditMode) {
      this.loadInstructors()
    }
  }

  openAddPopup = () => this.addPopupVisible = true;
  closeAddPopup = () => this.addPopupVisible = false;

  openEditPopup = (e: any) => {
    e.cancel = true;
    this.workoutDetails = new RealWorkout(e.appointmentData);
    const data = e.appointmentData;
    this.realWorkoutCommand = new RealWorkoutCommand(data.id, data.maxParticipantNumber,
      new Date(data.startDate), data.startDate, data.endDate, data.instructorId);
    this.popupHeight = 345;
    this.isEditMode = false;
    this.editPopupVisible = true;
  }

  closeEditPopup = () => this.editPopupVisible = false;
  openDeletePopup = () => this.isDeletePopupVisible = true;

  navigateToReservation = (reservationId: number | undefined) => this.router.navigate([pageUrls.reservations + '/', reservationId]);

  displayInstructorName = (instructor: Instructor) => `${instructor.firstName} ${instructor.lastName}`;

  closeEditPopupText = () => this.isEditMode ? "Cancel" : "Back";

  onAppointmentFormOpening = (e: any) => e.cancel = true;

  schedulerInitialize = (e: { component: dxScheduler }) => this.schedulerInit = e.component;
  editPopupFormInitialize = (e: { component: dxForm }) => this.editFormInit = e.component;
}
