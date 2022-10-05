import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
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
  registerFormData: RegisterForm = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
    dateOfBirth: ''
  };
  gender: EnumObject[]  = [
    { index: 1, description: 'Female' },
    { index: 2, description: 'Male' }
  ];
  namePattern: any = /^[^0-9]+$/;
  buttonOptions: any = {
    text: "Register",
    useSubmitBehavior: true,
    type: "success",
  };
  dateOptions: any = {
    invalidDateMessage:'The date must have the following format: dd-MM-yyyy',
    type: 'date',
    displayFormat:'dd-MM-yyyy',
    min: new Date(1900, 0, 1),
    max: new Date(Date.now())
  };
  apiUrl = environment.apiUrl;

  constructor(private userService: UserService, private router: Router) {
    super();
  }

  passwordComparison = () => this.registerFormData.password;

  signUp(data: RegisterForm) {
    this.userService.register(data).subscribe(response => {
    },
    error => {
      console.log(error);
    })
  }

}