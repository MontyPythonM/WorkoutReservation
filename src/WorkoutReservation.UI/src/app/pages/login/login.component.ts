import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginForm } from 'src/app/models/login-form.model';
import { BaseComponent } from 'src/app/common/base.component';

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
  //returnUrl: string;

  constructor(private userService: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
    super();
    this.loggedIn = false;
    this.loginData = { email: '', password: ''};
    //this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/shop';
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
          console.log("login success!");
        },
        error: () => {
          console.log("login failed!");
        }
      });
    }
  }
}
