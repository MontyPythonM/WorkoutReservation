import {Component} from '@angular/core';
import {UserService} from 'src/app/services/user.service';
import {BaseComponent} from 'src/app/common/base.component';
import {NotificationService} from 'src/app/services/notification.service';
import {Router} from "@angular/router";
import {LoginForm} from "../../models/interfaces/login-form.model";

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

  constructor(private userService: UserService,
              private notificationService: NotificationService,
              private router: Router) {
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
        next: () => {
          this.notificationService.show('Successfully logged in!', 'success');
          this.router.navigateByUrl('/home')
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
