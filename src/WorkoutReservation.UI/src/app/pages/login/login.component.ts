import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  {
  emailTextBox: any;
  passwordTextBox: any;
  email: string;
  password: string;

  constructor() {
    this.emailTextBox = {
      icon: "email",
      type: 'back',
      location: "after",
      stylingMode: 'text',
      hoverStateEnabled: false,
      focusStateEnabled: false,
      activeStateEnabled: false,  
    }

    this.passwordTextBox = {
      icon: "lock",
      type: 'back',
      location: "after",
      stylingMode: 'text',
      hoverStateEnabled: false,
      focusStateEnabled: false,
      activeStateEnabled: false,
    }

    this.email = '';
    this.password = ''; 
  }

  signIn(params: any) { 
    let result = params.validationGroup.validate();

    if(result.isValid) {
      console.log('email:', this.email);    
      console.log('password:', this.password);
      console.log('send request to backend');
    }
  }
}
