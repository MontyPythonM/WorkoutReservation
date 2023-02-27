import {Component, OnInit} from '@angular/core';
import Form from 'devextreme/ui/form';
import {ActivatedRoute, Router} from '@angular/router';
import {BaseComponent} from 'src/app/common/base.component';
import {InstructorDetailsCommand} from 'src/app/models/instructor-details-command.model';
import {InstructorDetails} from 'src/app/models/instructor-details.model';
import {InstructorService} from 'src/app/services/instructor.service';
import {EnumObject, enumToObjects} from 'src/app/models/enums/enum-converter';
import {Gender} from 'src/app/models/enums/gender.enum';
import {Permission} from "../../../models/enums/permission.enum";
import {pageUrls} from "../../../../environments/page-urls";

@Component({
  selector: 'app-instructor-details',
  templateUrl: './instructor-details.component.html',
  styleUrls: ['./instructor-details.component.css']
})
export class InstructorDetailsComponent extends BaseComponent implements OnInit {
  instructor?: InstructorDetails;
  instructorCommand!: InstructorDetailsCommand;
  isEditPopupOpened: boolean;
  isDeletePopupVisible: boolean;
  isSaving: boolean;
  instructorId: number;
  gender: EnumObject[];
  permissions = Permission;
  private form!: Form | undefined;

  constructor(private instructorService: InstructorService,
    private route: ActivatedRoute,
    private router: Router) {
    super();
    this.isEditPopupOpened = false;
    this.isDeletePopupVisible = false;
    this.isSaving = false;
    this.instructorId = this.route.snapshot.params['id'];
    this.gender = enumToObjects(Gender);
  }

  ngOnInit(): void {
    this.loadInstructorDetails(this.instructorId);
  }

  loadInstructorDetails(id: number): void {
    this.subscribe(this.instructorService.getDetails(id), {
      next: (response: InstructorDetails) => {
        this.instructor = response;
      }
    });
  }

  deleteInstructor() {
    this.subscribe(this.instructorService.remove(this.instructor!.id), {
      next: () => {
        this.notificationService.show('Instructor delete successfully', 'success')
        this.router.navigate(['instructors']);
      },
      error: () => {
        this.notificationService.show('Failed to delete instructor', 'error')
      }
    });
  }

  editInstructor = () => {
    if(!this.form?.validate().isValid) return;
    this.isSaving = true;
    this.subscribe(this.instructorService.update(this.instructorCommand), {
      next: () => {
        this.isSaving = false;
        this.closeEditPopup();
      },
      error: () => {
        this.notificationService.show('Failed to update instructor', 'error');
        this.isSaving = false;
      },
      complete: () => {
        this.notificationService.show('Instructor update successfully', 'success')
        this.ngOnInit();
      }
    });
  }

  openEditPopup = () => {
    this.instructorCommand = new InstructorDetailsCommand(
      this.instructor?.id,
      this.instructor?.firstName,
      this.instructor?.lastName,
      this.gender.find(x => x.value === this.instructor?.gender)?.index!,
      this.instructor?.biography,
      this.instructor?.email
    );
    this.isEditPopupOpened = true;
  }

  backToInstructors = () => this.router.navigateByUrl(pageUrls.instructors);

  closeEditPopup = () => this.isEditPopupOpened = false;
  openDeletePopup = () => this.isDeletePopupVisible = true;

  onFormInitialized = (e: any) => this.form = e.component;
}
