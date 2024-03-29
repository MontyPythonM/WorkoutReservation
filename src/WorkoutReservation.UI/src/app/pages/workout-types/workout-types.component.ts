import {Component, OnInit} from '@angular/core';
import dxForm from 'devextreme/ui/form';
import {BaseComponent} from 'src/app/common/base.component';
import {PagedQuery} from 'src/app/models/paged-query.model';
import {PagedResult} from 'src/app/models/paged-result.model';
import {WorkoutType} from 'src/app/models/workout-types.model';
import {WorkoutTypeService} from 'src/app/services/workout-type.service';
import {workoutIntensities} from "../../models/enums/workout-intensity.enum";
import {WorkoutTypeTagService} from "../../services/workout-type-tag.service";
import {InstructorService} from "../../services/instructor.service";
import {Instructor} from "../../models/instructor.model";
import {WorkoutTypeCommand} from "../../models/workout-types-command.model";
import {WorkoutTypeTagActive} from "../../models/workout-type-tag-active.model";
import {Permission} from "../../models/enums/permission.enum";
import {SortBySelector} from "../../models/enums/sort-by-selector.enum";

@Component({
  selector: 'app-workout-types',
  templateUrl: './workout-types.component.html',
  styleUrls: ['./workout-types.component.css']
})
export class WorkoutTypesComponent extends BaseComponent implements OnInit {
  workoutTypes?: PagedResult<WorkoutType>;
  workoutTypeCommand?: WorkoutTypeCommand;
  queryParams: PagedQuery;
  isCreatePopupOpened: boolean;
  isUpdatePopupOpened: boolean;
  isDeletePopupVisible: boolean;
  isSaving: boolean;
  intensities = workoutIntensities;
  sortBy = SortBySelector;
  workoutTypeTags: WorkoutTypeTagActive[];
  activeWorkoutTypeTags: WorkoutTypeTagActive[];
  activeAndExistingWorkoutTypeTags: WorkoutTypeTagActive[];
  instructors: Instructor[];
  workoutTypeIdToDelete!: number;
  permissions = Permission;
  private createPopupForm?: dxForm;
  private updatePopupForm?: dxForm;

  constructor(private workoutTypeService: WorkoutTypeService,
              private workoutTypeTagService: WorkoutTypeTagService,
              private instructorService: InstructorService) {
    super();
    this.workoutTypes = new PagedResult<WorkoutType>();
    this.queryParams = new PagedQuery({
      pageNumber: 1,
      pageSize: 10,
      sortByDescending: false,
      sortBy: this.sortBy.WorkoutName,
      searchPhrase: ''
    });
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
    this.isDeletePopupVisible = false;
    this.isSaving = false;
    this.workoutTypeTags = [];
    this.activeWorkoutTypeTags = [];
    this.activeAndExistingWorkoutTypeTags = [];
    this.instructors = [];
  }

  ngOnInit(): void {
    this.loadInstructors();
    this.loadWorkoutTypeTags();
    this.loadWorkoutTypes(this.queryParams);
  }

  protected loadWorkoutTypes(queryParams: PagedQuery): void {
    this.subscribe(this.workoutTypeService.getAllWithPagination(queryParams), {
      next: (response: PagedResult<WorkoutType>) => this.workoutTypes = response
    });
  }

  protected loadWorkoutTypeTags(): void {
    this.subscribe(this.workoutTypeTagService.getActive(), {
      next: (response: WorkoutTypeTagActive[]) => {
        this.workoutTypeTags = response;
        this.activeWorkoutTypeTags = response.filter(x => x.isActive);
      }
    });
  }

  protected loadInstructors(): void {
    this.subscribe(this.instructorService.getAll(), {
      next: (response: Instructor[]) => this.instructors = response
    });
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
    this.activeAndExistingWorkoutTypeTags = this.getActiveAndExistingTags(workoutType);
    this.workoutTypeCommand = new WorkoutTypeCommand(
      workoutType.id,
      workoutType.name,
      workoutType.description,
      this.intensities.find(x => x.value === workoutType.intensity)!.index,
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

  getWorkoutTag = (workoutTagId: number): string => this.workoutTypeTags.find(x => x.id === workoutTagId)!.tag;

  getInstructor = (instructorId: any): string => this.instructors.find(x => x.id === instructorId)?.name!;

  pageSizeChanged(e: any) {
    this.queryParams.pageSize = e;
    this.loadWorkoutTypes(this.queryParams);
  }

  pageNumberChanged(e: any) {
    this.queryParams.pageNumber = e;
    this.loadWorkoutTypes(this.queryParams);
  }

  private getActiveAndExistingTags = (workoutType: WorkoutType): WorkoutTypeTagActive[] => {
    const activeTags = this.activeWorkoutTypeTags;
    const existingNotActiveTagIds = workoutType.workoutTypeTags.filter(tagId => !activeTags.map(x => x.id).includes(tagId));
    const existingTags = this.workoutTypeTags.filter(tag => existingNotActiveTagIds.includes(tag.id));

    return [...activeTags, ...existingTags];
  }

  searchPhraseChanged = (value: string) => this.queryParams.searchPhrase = value;
  sortByChanged = (value: string) => this.queryParams.sortBy = value;
  orderByChanged = (value: boolean) => this.queryParams.sortByDescending = value;
}
