import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  buttonOptions = {
    text: "Register",
    useSubmitBehavior: true,
    type: "success",
  };
  dateOptions = {
    invalidDateMessage:'The date must have the following format: dd/MM/yyyy', 
    displayFormat:'dd.MM.yyyy'
  };
  namePattern: any = /^[^0-9]+$/;
  maxDate: Date = new Date(Date.now());
  registerFormData: any = { 
    firstName: '',
    lastName: '',
    gender: '',
    email: '',
    password: '',
    confirmPassword: ''
  };

  asyncValidation() {
    console.log("Send request to backend to check is email already exist");
  }

  passwordComparison = () => this.registerFormData.password;

  onFormSubmit(e: Event) {
    console.log('registerFormData: ', this.registerFormData);  
    e.preventDefault();
  }
}