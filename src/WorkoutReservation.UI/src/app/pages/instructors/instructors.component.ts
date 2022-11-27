import { Component, OnInit } from '@angular/core';
import Form from 'devextreme/ui/form';
import { BaseComponent } from 'src/app/common/base.component';
import { EnumObject, enumToObjects } from 'src/app/models/enums/enum-converter';
import { Gender } from 'src/app/models/enums/gender.enum';
import { InstructorDetailsCommand } from 'src/app/models/instructor-details-command.model';
import { Instructor } from 'src/app/models/instructor.model';
import { InstructorService } from 'src/app/services/instructor.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-instructors',
  templateUrl: './instructors.component.html',
  styleUrls: ['./instructors.component.css']
})
export class InstructorsComponent extends BaseComponent implements OnInit {
  instructors?: Instructor[];
  isPopupOpened: boolean;
  isSaving: boolean;
  instructorCommand!: InstructorDetailsCommand;
  gender: EnumObject[];
  private form!: Form | undefined;

  constructor(private instructorService: InstructorService,
              private notificationService: NotificationService) {
    super();
    this.instructors = new Array<Instructor>();
    this.isPopupOpened = false;
    this.isSaving = false;
    this.gender = enumToObjects(Gender);
  }

  ngOnInit(): void {
    this.loadInstructors();
  }

  loadInstructors(): void {
    this.subscribe(this.instructorService.getAll(), {
      next: (response: Instructor[]) => {
        this.instructors = response;
      }
    });
  }

  createInstructor = () => {
    if(!this.form?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.instructorService.create(this.instructorCommand), {
      next: () => {
        this.isSaving = false;
        this.closePopup();
      },
      error: () => {
        this.notificationService.show('Failed to add instructor', 'error');
        this.isSaving = false;
      },
      complete: () => {
        this.notificationService.show('Instructor added successfully', 'success')
        this.ngOnInit();
      }
    });
  }

  openPopup = () => this.isPopupOpened = true;
  closePopup = () => this.isPopupOpened = false;

  onFormInitialized = (e: any) => this.form = e.component;
}
