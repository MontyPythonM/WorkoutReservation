import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {AccountService} from "../../../services/identity/account.service";

@Component({
  selector: 'app-nav-bar',
  template: `
    <nav class="nav">

      <div class="nav-left">
        <a class="item" routerLink="home">Home</a>
        <a class="item" routerLink="workouts">Workouts</a>
        <a class="item" routerLink="instructors">Instructors</a>
        <a class="item" routerLink="workout-types">Workout types</a>
        <a *ngIf="accountService.isLogged$ | async" class="item" routerLink="reservations">My reservations</a>
        <a *ngIf="accountService.isLogged$ | async" class="item" routerLink="administration">Administration</a>
      </div>

      <div class="nav-right">
        <a *ngIf="!(accountService.isLogged$ | async)" class="item" routerLink="register">Register</a>
        <a *ngIf="!(accountService.isLogged$ | async)" class="item" routerLink="login">Login</a>
        <a *ngIf="accountService.isLogged$ | async" class="logged" routerLink="account-settings">Logged as: Mateusz Szewczyk</a>
        <a *ngIf="accountService.isLogged$ | async" class="logout" (click)="onLogout()">Logout</a>
      </div>

    </nav>
  `,
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends BaseComponent {

  constructor(public accountService: AccountService) {
    super();
  }

  onLogout = () => this.accountService.logout();
}
