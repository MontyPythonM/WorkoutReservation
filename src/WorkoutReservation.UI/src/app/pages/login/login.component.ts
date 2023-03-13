import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {Router} from "@angular/router";
import {LoginForm} from "../../models/interfaces/login-form.model";
import {AccountService} from "../../services/identity/account.service";
import {pageUrls} from "../../../environments/page-urls";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent{
  emailTextBox: any;
  passwordTextBox: any;
  loginData: LoginForm;

  constructor(private accountService: AccountService,
              private router: Router) {
    super();
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
      this.subscribe(this.accountService.login(this.loginData), {
        next: () => {
          this.notificationService.show('Successfully logged in!', 'success');
          this.router.navigate([pageUrls.workouts])
        },
        error: (error) => {
          if(error.status === 409) {
            this.notificationService.show('Invalid email address or password.', 'error');
          }
          else if(error.status !== 200) {
            this.notificationService.show('Unknown error occured.', 'error');
          }
        }
      });
    }
  }
}
