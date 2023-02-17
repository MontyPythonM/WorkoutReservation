import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {WorkoutTypeTag} from "../../../models/workout-type-tag.model";
import {WorkoutTypeTagService} from "../../../services/workout-type-tag.service";
import Form from "devextreme/ui/form";
import {NotificationService} from "../../../services/notification.service";
import {DATETIME_FORMAT} from "../../../constants/constants";
import {WorkoutTypeTagCommand} from "../../../models/workout-type-tag-command.model";
import {Row} from "devextreme/ui/data_grid";
import {Permission} from "../../../models/enums/permission.enum";

@Component({
  selector: 'app-workout-type-tags',
  templateUrl: './workout-type-tags.component.html',
  styleUrls: ['./workout-type-tags.component.css']
})
export class WorkoutTypeTagsComponent extends BaseComponent implements OnInit {

  workoutTypeTags: WorkoutTypeTag[];
  workoutTypeTagCommand?: WorkoutTypeTagCommand;
  isAddPopupVisible: boolean;
  isEditPopupVisible: boolean;
  isDeletePopupVisible: boolean;
  isSaving: boolean;
  dateFormat = DATETIME_FORMAT;
  isActiveTypes: { name: string, value: boolean }[];
  permissions = Permission;
  private addPopupform!: Form | undefined;
  private updatePopupForm!: Form | undefined;
  private workoutTypeTagIdToDelete?: number;

  constructor(private workoutTypeTagService: WorkoutTypeTagService) {
    super();
    this.workoutTypeTags = [];
    this.isAddPopupVisible = false;
    this.isEditPopupVisible = false;
    this.isDeletePopupVisible = false;
    this.isSaving = false;
    this.isActiveTypes = [
      { name: "true", value: true },
      { name: "false", value: false },
    ];
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
    if(!this.addPopupform?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.workoutTypeTagService.create(this.workoutTypeTagCommand?.tag!), {
      next: () => {
        this.isSaving = false;
        this.notificationService.show('Workout type tag added successfully', 'success')
        this.closeAddPopup();
        this.ngOnInit();
      },
      error: () => {
        this.notificationService.show('Failed to add workout type tag', 'error');
        this.isSaving = false;
      }
    });
  }

  openAddPopup = () => {
    this.addPopupform?.resetValues();
    this.isAddPopupVisible = true;
  }

  closeAddPopup = () => this.isAddPopupVisible = false;

  editWorkoutTypeTag = (): void => {
    if (!this.updatePopupForm?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.workoutTypeTagService.update(this.workoutTypeTagCommand!), {
      next: () => {
        this.notificationService.show('Workout type tag updated successfully', 'success');
        this.isSaving = false;
        this.closeEditPopup();
        this.ngOnInit();
      },
      error: () => {
        this.notificationService.show('Failed to update workout type tag', 'error');
        this.isSaving = false;
      }
    });
  }

  openEditPopup = (e: {row: Row}) => {
    const row = e.row.data;
    this.workoutTypeTagCommand = new WorkoutTypeTagCommand(row.id, row.tag, row.isActive);
    this.isEditPopupVisible= true;
  }
  closeEditPopup = () => this.isEditPopupVisible = false;

  deleteWorkoutTypeTag = () => {
    this.subscribe(this.workoutTypeTagService.remove(this.workoutTypeTagIdToDelete!), {
      next: () => {
        this.notificationService.show('Workout type tag delete successfully', 'success');
        this.ngOnInit();
      },
      error: () => this.notificationService.show('Failed to delete workout type tag', 'error')
    });
  }

  openDeletePopup = (e: {row: Row}) => {
    this.workoutTypeTagIdToDelete = e.row.data.id;
    this.isDeletePopupVisible = true;
  }

  updatePopupFormInitialize = (e: {component: Form}) => this.updatePopupForm = e.component;
  createPopupFromInitialize = (e: {component: Form}) => this.addPopupform = e.component;
}
