import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {BaseComponent} from 'src/app/common/base.component';
import {DATE_FORMAT, DATEONLY_FORMAT, TIME_FORMAT} from 'src/app/common/constants';
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
import {WorkoutType} from "../../models/workout-types.model";
import {WorkoutTypeService} from "../../services/workout-type.service";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrls: ['./workouts.component.css']
})
export class WorkoutsComponent extends BaseComponent implements OnInit {
  realWorkouts: RealWorkout[];
  realWorkoutDetails?: RealWorkout;
  instructors?: Instructor[];
  workoutTypes?: WorkoutType[];
  realWorkoutCommand?: RealWorkoutCommand;
  dateDisplayFormat: string;
  dateOnlyFormat: string;
  timeDisplayFormat: string;
  permissions = Permission;
  isEditMode: boolean;
  addPopupVisible: boolean;
  editPopupVisible: boolean;
  deletePopupVisible: boolean;
  editPopupHeight: number;
  schedulerInit?: dxScheduler;
  editFormInit?: dxForm;
  addFormInit?: dxForm;

  constructor(private realWorkoutService: RealWorkoutService,
              private router: Router,
              private reservationService: ReservationService,
              private instructorService: InstructorService,
              private workoutTypeService: WorkoutTypeService,
              private datePipe: DatePipe) {
    super();
    this.realWorkouts = [];
    this.dateDisplayFormat = DATE_FORMAT;
    this.dateOnlyFormat = DATEONLY_FORMAT;
    this.timeDisplayFormat = TIME_FORMAT;
    this.editPopupVisible = false;
    this.isEditMode = false;
    this.addPopupVisible = false;
    this.editPopupHeight = 345;
    this.deletePopupVisible = false;
  }

  ngOnInit(): void {
    this.loadRealWorkouts();
  }

  loadRealWorkouts(): void {
    this.subscribe(this.realWorkoutService.getRealWorkouts(), {
      next: (result: RealWorkout[]) => this.realWorkouts = result
    });
  }

  loadInstructors() {
    this.subscribe(this.instructorService.getAll(), {
      next: (result: Instructor[]) => this.instructors = result
    });
  }

  loadWorkoutTypes() {
    this.subscribe(this.workoutTypeService.getAll(), {
      next: (result: WorkoutType[]) => this.workoutTypes = result
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
    if (this.addFormInit!.validate().isValid) {
      this.subscribe(this.realWorkoutService.create(this.realWorkoutCommand!), {
        next: () => {
          this.ngOnInit();
          this.notificationService.show("Workout successfully created", "success");
          this.closeAddPopup();
        }
      });
    }
  }

  editRealWorkout() {
    if (this.editFormInit!.validate().isValid) {
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
    this.editPopupHeight = 440;
    this.isEditMode = !this.isEditMode;
    if (this.isEditMode) {
      this.loadInstructors()
    }
  }

  openAddPopup = () => {
    this.loadInstructors();
    this.loadWorkoutTypes();
    this.realWorkoutCommand = new RealWorkoutCommand();
    this.addPopupVisible = true;
  }

  closeAddPopup = () => this.addPopupVisible = false;

  openEditPopup = (e: any) => {
    e.cancel = true;
    this.realWorkoutDetails = new RealWorkout(e.appointmentData);
    const data = e.appointmentData;
    this.realWorkoutCommand = new RealWorkoutCommand(data.id, data.maxParticipantNumber,
      this.toDateOnly(data.startDate), data.startDate, data.endDate, data.instructorId);
    this.editPopupHeight = 345;
    this.isEditMode = false;
    this.editPopupVisible = true;
  }

  closeEditPopup = () => this.editPopupVisible = false;

  closeEditPopupText = () => this.isEditMode ? "Cancel" : "Back";

  openDeletePopup = () => this.deletePopupVisible = true;

  navigateToReservation = (reservationId: number | undefined) => this.router.navigate([pageUrls.reservations + '/', reservationId]);

  onAppointmentFormOpening = (e: any) => e.cancel = true;

  maxParticipants = (): number => this.realWorkoutDetails?.currentParticipantNumber! > 1 ? this.realWorkoutDetails?.currentParticipantNumber! : 1;

  schedulerInitialize = (e: { component: dxScheduler }) => this.schedulerInit = e.component;
  editPopupFormInitialize = (e: { component: dxForm }) => this.editFormInit = e.component;
  addPopupFormInitialize = (e: { component: dxForm }) => this.addFormInit = e.component;

  private toDateOnly = (dateTime: Date | string): string => this.datePipe.transform(dateTime, DATEONLY_FORMAT)!;
}
