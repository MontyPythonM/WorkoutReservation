import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  {
  emailTextBox: any;
  passwordTextBox: any;
  loginButton: any;

  constructor() {
    this.emailTextBox = {
      icon: "email",
      type: 'back',
      location: "after",
      stylingMode: 'text',
      hoverStateEnabled: false,
      focusStateEnabled: false,
      activeStateEnabled: false
    }

    this.passwordTextBox = {
      icon: "lock",
      type: 'back',
      location: "after",
      stylingMode: 'text',
      hoverStateEnabled: false,
      focusStateEnabled: false,
      activeStateEnabled: false
    }

    this.loginButton = {
      
    }
  }

}
