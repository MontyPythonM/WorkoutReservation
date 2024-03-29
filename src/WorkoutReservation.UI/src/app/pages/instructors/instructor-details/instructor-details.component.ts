import {Component, OnInit} from '@angular/core';
import Form from 'devextreme/ui/form';
import {ActivatedRoute, Router} from '@angular/router';
import {BaseComponent} from 'src/app/common/base.component';
import {InstructorDetailsCommand} from 'src/app/models/instructor-details-command.model';
import {InstructorDetails} from 'src/app/models/instructor-details.model';
import {InstructorService} from 'src/app/services/instructor.service';
import {genders} from 'src/app/models/enums/gender.enum';
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
  isEditPopupVisible: boolean;
  isDeletePopupVisible: boolean;
  isSaving: boolean;
  instructorId: number;
  genders = genders;
  permissions = Permission;
  private form!: Form | undefined;

  constructor(private instructorService: InstructorService,
    private route: ActivatedRoute,
    private router: Router) {
    super();
    this.isEditPopupVisible = false;
    this.isDeletePopupVisible = false;
    this.isSaving = false;
    this.instructorId = this.route.snapshot.params['id'];
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
        this.backToInstructors();
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
      this.genders.find(x => x.value === this.instructor?.gender)?.index!,
      this.instructor?.biography,
      this.instructor?.email
    );
    this.isEditPopupVisible = true;
  }

  backToInstructors = () => this.router.navigate([pageUrls.instructors]);

  closeEditPopup = () => this.isEditPopupVisible = false;
  openDeletePopup = () => this.isDeletePopupVisible = true;

  onFormInitialized = (e: any) => this.form = e.component;
}
