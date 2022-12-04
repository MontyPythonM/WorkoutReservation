import {Component, OnInit} from '@angular/core';
import Form from 'devextreme/ui/form';
import {BaseComponent} from 'src/app/common/base.component';
import {PagedQuery} from 'src/app/models/paged-query.model';
import {PagedResult} from 'src/app/models/paged-result.model';
import {WorkoutType} from 'src/app/models/workout-types.model';
import {WorkoutTypeService} from 'src/app/services/workout-type.service';
import {NotificationService} from "../../services/notification.service";
import {EnumObject, enumToObjects} from "../../models/enums/enum-converter";
import {WorkoutIntensity} from "../../models/enums/workout-intensity.enum";
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
  isSaving: boolean;
  intensity: EnumObject[];
  private form!: Form | undefined;

  constructor(private workoutTypeService: WorkoutTypeService,
              private notificationService: NotificationService) {
    super();
    this.workoutTypes = new PagedResult<WorkoutType>();
    this.query = PagedQuery.default();
    this.query.sortBy = 'Name';
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
    this.isSaving = false;
    this.intensity = enumToObjects(WorkoutIntensity);
  }

  ngOnInit(): void {
    this.loadWorkoutTypes(this.query);
  }

  loadWorkoutTypes(queryParams: PagedQuery): void {
    this.subscribe(this.workoutTypeService.getAll(queryParams), {
      next: (response: PagedResult<WorkoutType>) => {
        this.workoutTypes = response;
      }
    });
  }

  createWorkoutType() {
    if (!this.form?.validate().isValid) return;
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
    if (!this.form?.validate().isValid) return;
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
        this.notificationService.show('Workout type updated successfully', 'success')
        this.workoutTypeCommand = new WorkoutTypeCommand();
        this.ngOnInit();
      }
    });
  }

  deleteWorkoutType(id: number) {
    this.subscribe(this.workoutTypeService.remove(id), {
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
      this.intensity.find(x => x.value === workoutType.intensity)!.index
    );
    this.isUpdatePopupOpened = true;
  }

  closePopup = () => {
    this.workoutTypeCommand = new WorkoutTypeCommand();
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
  }

  onFormInitialized = (e: any) => this.form = e.component;
}
