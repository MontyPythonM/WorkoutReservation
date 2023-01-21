import {Component, OnInit} from '@angular/core';
import dxForm from 'devextreme/ui/form';
import {BaseComponent} from 'src/app/common/base.component';
import {PagedQuery} from 'src/app/models/paged-query.model';
import {PagedResult} from 'src/app/models/paged-result.model';
import {WorkoutType} from 'src/app/models/workout-types.model';
import {WorkoutTypeService} from 'src/app/services/workout-type.service';
import {NotificationService} from "../../services/notification.service";
import {EnumObject, enumToObjects} from "../../models/enums/enum-converter";
import {WorkoutIntensity} from "../../models/enums/workout-intensity.enum";
import {WorkoutTypeTagService} from "../../services/workout-type-tag.service";
import {WorkoutTypeTag} from "../../models/workout-type-tag.model";
import {InstructorService} from "../../services/instructor.service";
import {Instructor} from "../../models/instructor.model";
import {WorkoutTypeCommand} from "../../models/workout-types-command.model";

@Component({
  selector: 'app-workout-types',
  templateUrl: './workout-types.component.html',
  styleUrls: ['./workout-types.component.css']
})
export class WorkoutTypesComponent extends BaseComponent implements OnInit {
  workoutTypes?: PagedResult<WorkoutType>;
  workoutTypeCommand?: WorkoutTypeCommand;
  query: PagedQuery;
  isCreatePopupOpened: boolean;
  isUpdatePopupOpened: boolean;
  isDeletePopupVisible: boolean;
  isSaving: boolean;
  intensity: EnumObject[];
  workoutTypeTags: WorkoutTypeTag[];
  instructors: Instructor[];
  workoutTypeIdToDelete!: number;
  private createPopupForm?: dxForm;
  private updatePopupForm?: dxForm;

  constructor(private workoutTypeService: WorkoutTypeService,
              private notificationService: NotificationService,
              private workoutTypeTagService: WorkoutTypeTagService,
              private instructorService: InstructorService) {
    super();
    this.workoutTypes = new PagedResult<WorkoutType>();
    this.query = new PagedQuery({
      pageNumber: 1,
      pageSize: 10,
      sortByDescending: false,
      sortBy: 'Name',
      searchPhrase: ''
    });
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
    this.isDeletePopupVisible = false;
    this.isSaving = false;
    this.intensity = enumToObjects(WorkoutIntensity);
    this.workoutTypeTags = [];
    this.instructors = [];
  }

  ngOnInit(): void {
    this.loadInstructors();
    this.loadWorkoutTypeTags();
    this.loadWorkoutTypes(this.query);
  }

  protected loadWorkoutTypes(queryParams: PagedQuery): void {
    this.subscribe(this.workoutTypeService.getAll(queryParams), {
      next: (response: PagedResult<WorkoutType>) => this.workoutTypes = response
    });
  }

  protected loadWorkoutTypeTags(): void {
    this.subscribe(this.workoutTypeTagService.getAllWorkoutTypeTags(), {
      next: (response: WorkoutTypeTag[]) => this.workoutTypeTags = response
    })
  }

  protected loadInstructors(): void {
    this.subscribe(this.instructorService.getAll(), {
      next: (response: Instructor[]) => this.instructors = response
    })
  }

  createWorkoutType() {
    if (!this.createPopupForm?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.workoutTypeService.create(this.workoutTypeCommand!), {
      next: () => {
        this.isSaving = false;
        this.closePopup();
      },
      error: () => {
        this.notificationService.show('Failed to add workout type', 'error');
        this.isSaving = false;
      },
      complete: () => {
        this.notificationService.show('Workout type added successfully', 'success')
        this.ngOnInit();
      }
    });
  }

  updateWorkoutType() {
    if (!this.updatePopupForm?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.workoutTypeService.update(this.workoutTypeCommand!), {
      next: () => {
        this.isSaving = false;
        this.closePopup();
      },
      error: () => {
        this.notificationService.show('Failed to update workout type', 'error');
        this.isSaving = false;
      },
      complete: () => {
        this.notificationService.show('Workout type updated successfully', 'success');
        this.ngOnInit();
      }
    });
  }

  deleteWorkoutType() {
    this.subscribe(this.workoutTypeService.remove(this.workoutTypeIdToDelete), {
      next: () => {
        this.notificationService.show('Workout type delete successfully', 'success');
        this.ngOnInit();
      },
      error: () => {
        this.notificationService.show('Failed to delete workout type', 'error')
      }
    });
  }

  openCreatePopup = () => {
    this.workoutTypeCommand = new WorkoutTypeCommand();
    this.isCreatePopupOpened = true;
  }

  openUpdatePopup = (workoutType: WorkoutType) => {
    this.workoutTypeCommand = new WorkoutTypeCommand(
      workoutType.id,
      workoutType.name,
      workoutType.description,
      this.intensity.find(x => x.value === workoutType.intensity)!.index,
      workoutType.workoutTypeTags,
      workoutType.instructors
    );
    this.isUpdatePopupOpened = true;
  }

  openDeletePopup = (id: number) => {
    this.workoutTypeIdToDelete = id;
    this.isDeletePopupVisible = true;
  }

  closePopup = () => {
    this.workoutTypeCommand = new WorkoutTypeCommand();
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
  }

  onCreatePopupForm = (e: { component: dxForm }) => this.createPopupForm = e.component;
  onUpdatePopupForm = (e: { component: dxForm }) => this.updatePopupForm = e.component;

  getWorkoutTag = (workoutTagId: any): string => this.workoutTypeTags.find(x => x.id === workoutTagId)!.tag!;

  getInstructor = (instructorId: any): string => {
    const instructor = this.instructors.find(x => x.id === instructorId)!;
    return `${instructor.firstName} ${instructor.lastName}`;
  }

  pageSizeChanged(e: any) {
    this.query.pageSize = e;
    this.loadWorkoutTypes(this.query);
  }

  pageNumberChanged(e: any) {
    this.query.pageNumber = e;
    this.loadWorkoutTypes(this.query);
  }
}
