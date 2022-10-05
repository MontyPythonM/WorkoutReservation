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

  signIn(params: any) {
    const result = params.validationGroup.validate();

    if(result.isValid) {
      this.userService.login(this.loginData).subscribe(() => {
       // TODO: this.router.navigateByUrl(this.returnUrl); redirect to main view after login
      }, error => {
        console.log('error: ', error);
        if(error.status === 200) {
          // return token (error.error.text)
        }
      })
    }
  }
}
