import { Component } from '@angular/core';
import { Router } from '@angular/router';
import dxForm from 'devextreme/ui/form';
import { BaseComponent } from 'src/app/common/base.component';
import { EnumObject } from 'src/app/models/enum-object.model';
import { RegisterForm } from 'src/app/models/register-form.model';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent extends BaseComponent {
  registerFormData: RegisterForm;
  gender: EnumObject[];
  namePattern: any;
  dateOptions: any;
  private dxForm!: dxForm;

  constructor(private userService: UserService, private router: Router) {
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
    this.gender = [
      { index: 1, description: 'Female' },
      { index: 2, description: 'Male' }
    ];
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
          console.log('Register failed. Error: ', error);
        },
        complete: () => {
          this.router.navigate(['/login']);
        }
      });
    }
  }
}