import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {WorkoutTypeTag} from "../../../models/workout-type-tag.model";
import {WorkoutTypeTagService} from "../../../services/workout-type-tag.service";
import Form from "devextreme/ui/form";
import {NotificationService} from "../../../services/notification.service";

@Component({
  selector: 'app-workout-type-tags',
  templateUrl: './workout-type-tags.component.html',
  styleUrls: ['./workout-type-tags.component.css']
})
export class WorkoutTypeTagsComponent extends BaseComponent implements OnInit {

  workoutTypeTags: WorkoutTypeTag[];
  workoutTypeTagCommand?: WorkoutTypeTag;
  isAddPopupOpened: boolean;
  isSaving: boolean;

  private form!: Form | undefined;

  constructor(private workoutTypeTagService: WorkoutTypeTagService,
              private notificationService: NotificationService) {
    super();
    this.workoutTypeTags = [];
    this.isAddPopupOpened = false;
    this.isSaving = false;

  }

  ngOnInit(): void {
    this.loadWorkoutTypeTags();
  }

  loadWorkoutTypeTags(): void {
    this.subscribe(this.workoutTypeTagService.getAll(), {
      next: (response: WorkoutTypeTag[]) => this.workoutTypeTags = response
    });
  }

  createWorkoutTypeTag() {
    if(!this.form?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.workoutTypeTagService.create(this.workoutTypeTagCommand?.tag!), {
      next: () => {
        this.isSaving = false;
        this.closeAddPopup();
      },
      error: () => {
        this.notificationService.show('Failed to add workout type tag', 'error');
        this.isSaving = false;
      },
      complete: () => {
        this.notificationService.show('Workout type tag added successfully', 'success')
        this.ngOnInit();
      }
    });
  }

  openAddPopup = () => this.isAddPopupOpened = true;
  closeAddPopup = () => this.isAddPopupOpened = false;

  onFormInitialized = (e: {component: Form}) => this.form = e.component;
}
