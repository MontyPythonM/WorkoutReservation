import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginForm } from 'src/app/models/login-form.model';
import { BaseComponent } from 'src/app/common/base.component';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent{
  emailTextBox: any;
  passwordTextBox: any;
  loggedIn: boolean;
  loginData: LoginForm;

  constructor(private userService: UserService, private notificationService: NotificationService) {
    super();
    this.loggedIn = false;
    this.loginData = { email: '', password: ''};
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
  }

  signIn(form: any) {
    const validationResult = form.validationGroup.validate();
    if(validationResult.isValid) {
      this.subscribe(this.userService.login(this.loginData), {
        complete: () => {
          // route to home and set user loggedIn
        },
        error: (error) => {
          if(error.status !== 200) {
            this.notificationService.show('Invalid email address or password.', 'error');
          }
        }
      });
    }
  }
}
