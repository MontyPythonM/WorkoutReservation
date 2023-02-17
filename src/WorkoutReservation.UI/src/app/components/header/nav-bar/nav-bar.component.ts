import {Component, OnInit} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {AccountService} from "../../../services/identity/account.service";
import {Permission} from "../../../models/enums/permission.enum";
import {UserAccount} from "../../../models/user-account.model";

@Component({
  selector: 'app-nav-bar',
  template: `
    <nav class="nav">
      <div class="nav-left">
        <a class="item" routerLink="home">Home</a>
        <a class="item" routerLink="workouts">Workouts</a>
        <a class="item" routerLink="instructors">Instructors</a>
        <a class="item" routerLink="workout-types">Workout types</a>
        <a *ngIf="isAuthenticated()" class="item" routerLink="reservations">My reservations</a>
        <a *ngIf="isAuthenticated() && (hasPermission(permissions.OpenAdministrationPage) | async)" class="item" routerLink="administration">Administration</a>
      </div>
      <div class="nav-right">
        <a *ngIf="!isAuthenticated()" class="item" routerLink="register">Register</a>
        <a *ngIf="!isAuthenticated()" class="item" routerLink="login">Login</a>
        <a *ngIf="isAuthenticated()" class="logged" routerLink="account-settings">Logged as: {{ userFullName }}</a>
        <a *ngIf="isAuthenticated()" class="logout" (click)="onLogout()">Logout</a>
      </div>
    </nav>
  `,
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends BaseComponent implements OnInit {
  userFullName: string
  permissions = Permission;

  constructor(private accountService: AccountService) {
    super();
    this.userFullName = "-";
  }

  ngOnInit(): void {
    this.getUserFullName();
  }

  onLogout = () => this.accountService.logout();

  getUserFullName = () => super.subscribe(this.accountService.userAccount$, {
    next: (user: UserAccount) => this.userFullName = user.fullName
  });
}
