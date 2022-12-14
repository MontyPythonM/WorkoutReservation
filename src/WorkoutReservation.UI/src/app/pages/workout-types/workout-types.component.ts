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
import {WorkoutTypeTagService} from "../../services/workout-type-tag.service";
import {WorkoutTypeTag} from "../../models/workout-type-tag.model";

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
  workoutTypeTags: WorkoutTypeTag[];
  activeAndCurrentTags: WorkoutTypeTag[] = [];
  private form!: Form | undefined;

  constructor(private workoutTypeService: WorkoutTypeService,
              private notificationService: NotificationService,
              private workoutTypeTagService: WorkoutTypeTagService) {
    super();
    this.workoutTypes = new PagedResult<WorkoutType>();
    this.query = PagedQuery.default();
    this.query.sortBy = 'Name';
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
    this.isSaving = false;
    this.intensity = enumToObjects(WorkoutIntensity);
    this.workoutTypeTags = [];
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

  loadActiveWorkoutTypeTags(): void {
    this.subscribe(this.workoutTypeTagService.getAllWorkoutTypeTags(), {
      next: (response: WorkoutTypeTag[]) => {
        this.workoutTypeTags = response;
      }
    })
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
    this.loadActiveWorkoutTypeTags();
    this.workoutTypeCommand = new WorkoutTypeCommand();
    this.isCreatePopupOpened = true;
  }

  openUpdatePopup = (workoutType: WorkoutType) => {
    this.loadActiveWorkoutTypeTags();

    // returns array with active tags and existing non active tags
    let tags: WorkoutTypeTag[] = [];
    for (let i = 0; i < this.workoutTypeTags.length; i++) {
      let tag = this.workoutTypeTags[i];
      if (tag.isActive) {
        tags.push(tag);
      }
      for (let j =0; j < workoutType.workoutTypeTags.length; j++) {
        if (workoutType.workoutTypeTags[j].id === tag.id && !workoutType.workoutTypeTags[j].isActive) {
          tags.push(tag);
          continue;
        }
      }
    }
    // distinct a tags array
    this.activeAndCurrentTags = tags.filter(
      (thing, i, arr) => arr.findIndex(t => t.id === thing.id) === i
    );

    console.log('workoutType.workoutTypeTags: ', workoutType.workoutTypeTags);
    console.log('this.workoutTypeTags: ', this.workoutTypeTags);
    console.log('this.activeAndCurrentTags: ', this.activeAndCurrentTags);

    this.workoutTypeCommand = new WorkoutTypeCommand(
      workoutType.id,
      workoutType.name,
      workoutType.description,
      this.intensity.find(x => x.value === workoutType.intensity)!.index,
      workoutType.workoutTypeTags
    );
    this.isUpdatePopupOpened = true;
  }

  closePopup = () => {
    this.workoutTypeCommand = new WorkoutTypeCommand();
    this.isCreatePopupOpened = false;
    this.isUpdatePopupOpened = false;
  }

  onFormInitialized = (e: {component: Form}) => this.form = e.component;
}
