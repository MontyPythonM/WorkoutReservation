import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DxFormComponent } from 'devextreme-angular';
import dxForm from 'devextreme/ui/form';
import { BaseComponent } from 'src/app/common/base.component';
import { EnumObject, enumToObjects } from 'src/app/models/enums/enum-converter';
import { Gender } from 'src/app/models/enums/gender.enum';
import { RegisterForm } from 'src/app/models/register-form.model';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent extends BaseComponent {

  @ViewChild(DxFormComponent) registerForm!: DxFormComponent;
  registerFormData: RegisterForm;
  gender: EnumObject[];
  namePattern: any;
  dateOptions: any;
  private dxForm!: dxForm;
  private emailTakenErrorMessage = 'Validation failed: \r\n -- Email: Email address is already taken. Severity: Error'

  constructor(private userService: UserService, private router: Router, private notificationService: NotificationService) {
    super();
    this.registerFormData = new RegisterForm();
    this.namePattern = /^[^0-9]+$/;
    this.dateOptions = {
      invalidDateMessage:'The date must have the following format: dd-MM-yyyy',
      type: 'date',
      displayFormat:'dd-MM-yyyy',
      min: new Date(1900, 0, 1),
      max: new Date(Date.now())
    };
    this.gender = enumToObjects(Gender);
  }

  resetForm() {
    this.registerForm.instance.resetValues();
  }

  initializeDxForm(event: {component: dxForm}): void {
    this.dxForm = event.component;
  }

  passwordComparison = () => this.registerFormData.password;

  signUp() {
    const isValid = this.dxForm.validate().isValid;
    if(isValid) {
      this.subscribe(this.userService.register(this.registerFormData), {
        error: (error) => {
          this.registerFormData.email = '';
          this.registerFormData.dateOfBirth = '';
          error.error == this.emailTakenErrorMessage ?
            this.notificationService.show('Email address is already taken.', 'error') :
            this.notificationService.show('Registration failed!', 'error');
        },
        complete: () => {
          this.resetForm();
          this.notificationService.show('Account has been created.', 'success');
        }
      });
    }
  }
}