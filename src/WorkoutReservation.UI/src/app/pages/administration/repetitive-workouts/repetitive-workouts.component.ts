import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {RepetitiveWorkoutService} from "../../../services/repetitive-workout.service";
import {Permission} from "../../../models/enums/permission.enum";
import {DATE_FORMAT, DATEONLY_FORMAT, TIME_FORMAT} from "../../../common/constants";
import {Instructor} from "../../../models/instructor.model";
import {WorkoutType} from "../../../models/workout-types.model";
import {RepetitiveWorkout} from "../../../models/repetitive-workout.model";
import {RepetitiveWorkoutCommand} from "../../../models/repetitive-workout-command.model";
import dxScheduler from 'devextreme/ui/scheduler';
import {InstructorService} from "../../../services/instructor.service";
import {WorkoutTypeService} from "../../../services/workout-type.service";
import dxForm from 'devextreme/ui/form';
import {daysOfWeek} from "../../../models/enums/day-of-week.enum";

@Component({
  selector: 'app-repetitive-workouts',
  templateUrl: './repetitive-workouts.component.html',
  styleUrls: ['./repetitive-workouts.component.css']
})
export class RepetitiveWorkoutsComponent extends BaseComponent implements OnInit {
  repetitiveWorkouts: RepetitiveWorkout[];
  instructors?: Instructor[];
  workoutTypes?: WorkoutType[];
  repetitiveWorkoutCommand?: RepetitiveWorkoutCommand;
  isGeneratePopupVisible: boolean;
  isDeletePopupVisible: boolean;
  popupTitle: string;
  popupContent: string;
  permissions = Permission;
  dateDisplayFormat: string = DATE_FORMAT;
  dateOnlyFormat: string = DATEONLY_FORMAT;
  timeDisplayFormat: string = TIME_FORMAT;
  schedulerInit?: dxScheduler;
  addPopupVisible: boolean;
  editPopupVisible: boolean;
  deletePopupVisible: boolean;
  editFormInit?: dxForm;
  addFormInit?: dxForm;
  daysOfWeek = daysOfWeek;

  constructor(private repetitiveWorkoutService: RepetitiveWorkoutService,
              private instructorService: InstructorService,
              private workoutTypeService: WorkoutTypeService) {
    super();
    this.repetitiveWorkouts = [];
    this.isGeneratePopupVisible = false;
    this.isDeletePopupVisible = false;
    this.popupTitle = "";
    this.popupContent = "";
    this.editPopupVisible = false;
    this.addPopupVisible = false;
    this.deletePopupVisible = false;
  }

  ngOnInit(): void {
    this.loadRepetitiveWorkouts()
  }

  loadRepetitiveWorkouts() {
    this.subscribe(this.repetitiveWorkoutService.getRepetitiveWorkouts(), {
      next: (result: RepetitiveWorkout[]) => this.repetitiveWorkouts = result
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

  forceGenerateUpcomingWeek() {
    this.subscribe(this.repetitiveWorkoutService.generateUpcomingWeek(), {
      next: () => {
        this.notificationService.show("Workout generator has been launched. Check if the operation was successful!", "success");
        this.ngOnInit();
      },
      error: () => this.notificationService.show("Forced generation of repetitive workouts failed", "error")
    });
  }

  addRepetitiveWorkout() {
    if (this.addFormInit!.validate().isValid) {
      this.subscribe(this.repetitiveWorkoutService.create(this.repetitiveWorkoutCommand!), {
        next: () => {
          this.ngOnInit();
          this.notificationService.show("Repetitive workout successfully created", "success");
          this.closeAddPopup();
        }
      });
    }
  }

  editRepetitiveWorkout() {
    if (this.editFormInit!.validate().isValid) {
      this.subscribe(this.repetitiveWorkoutService.update(this.repetitiveWorkoutCommand!), {
        next: () => {
          this.ngOnInit();
          this.notificationService.show("Repetitive workout successfully updated", "success");
          this.closeEditPopup();
        }
      });
    }
  }

  deleteRepetitiveWorkout() {
    this.subscribe(this.repetitiveWorkoutService.remove(this.repetitiveWorkoutCommand?.id!), {
      next: () => {
        this.ngOnInit();
        this.editPopupVisible = false;
        this.notificationService.show("Repetitive workout successfully deleted", "success");
      }
    });
  }

  deleteAllRepetitiveWorkouts() {
    this.subscribe(this.repetitiveWorkoutService.deleteAll(), {
      next: () => {
        this.ngOnInit();
        this.notificationService.show("Successfully removed all repetitive workouts ", "success")
      },
      error: () => this.notificationService.show("Failed to remove all repetitive workouts", "error")
    });
  }

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

  openAddPopup = () => {
    this.loadInstructors();
    this.loadWorkoutTypes();
    this.addFormInit?.resetValues();
    this.repetitiveWorkoutCommand = new RepetitiveWorkoutCommand();
    this.addPopupVisible = true;
  }

  closeAddPopup = () => this.addPopupVisible = false;

  openEditPopup = (e: any) => {
    e.cancel = true;
    this.loadInstructors();
    this.loadWorkoutTypes();
    const data = e.appointmentData;
    this.repetitiveWorkoutCommand = new RepetitiveWorkoutCommand(data.id, data.maxParticipantNumber,
      data.startDate, data.endDate, data.dayOfWeek, data.workoutTypeId, data.instructorId);
    this.editPopupVisible = true;
  }

  isAnyRepetitiveWorkoutHasConflict(): boolean {
    return this.repetitiveWorkouts.some(rw => rw.isRealWorkoutConflict);
  }

  closeEditPopup = () => this.editPopupVisible = false;

  onAppointmentFormOpening = (e: any) => e.cancel = true;

  schedulerInitialize = (e: { component: dxScheduler }) => this.schedulerInit = e.component;
  editPopupFormInitialize = (e: { component: dxForm }) => this.editFormInit = e.component;
  addPopupFormInitialize = (e: { component: dxForm }) => this.addFormInit = e.component;
}
