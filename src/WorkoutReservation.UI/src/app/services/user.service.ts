import {DatePipe} from '@angular/common';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable} from 'rxjs';
import {RegisterForm} from '../models/register-form.model';
import {BaseService} from "../common/base.service";
import {apiUrl} from "../../environments/api-urls";
import {PagedResult} from "../models/paged-result.model";
import {Router} from "@angular/router";
import {JwtHelperService} from '@auth0/angular-jwt';
import {ApplicationUser} from "../models/application-user.model";
import {LoginForm} from "../models/interfaces/login-form.model";
import {StorageKeys} from "../common/storage-keys.enum";
import {NotificationService} from "./notification.service";
import {CurrentUser} from "../models/current-user.model";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(protected override http: HttpClient,
              private datePipe: DatePipe,
              private router: Router,
              private notificationService: NotificationService) {
    super(http);
  }

  public get isAuthenticated(): boolean {
    return !!sessionStorage.getItem(StorageKeys.Token);
  }

  login(loginForm: LoginForm): Observable<void> {
    return super.post<string>(apiUrl.account.login, loginForm, {}, {
      responseType: 'text'
    }).pipe(map((token: string) => sessionStorage.setItem(StorageKeys.Token, token)));
  }

  logout(): void {
    if (this.isAuthenticated) {
      sessionStorage.removeItem(StorageKeys.Token);
      sessionStorage.removeItem(StorageKeys.Permissions);
      this.notificationService.show("Successfully logged out", "success");
      this.router.navigateByUrl('/home');
    }
  }

  getToken(): string | null {
    const token = sessionStorage.getItem(StorageKeys.Token);
    const jwtHelper = new JwtHelperService();
    if (token && jwtHelper.isTokenExpired(token)) {
      this.logout();
    }
    return token;
  }

  register(registerForm: RegisterForm): Observable<string> {
    registerForm.dateOfBirth = this.datePipe.transform(registerForm.dateOfBirth, 'dd-MM-yyyy')!;
    return super.post<any>(apiUrl.account.register, registerForm);
  }

  getUsers(queryParams: any): Observable<PagedResult<ApplicationUser>> {
    return super.get<PagedResult<ApplicationUser>>(apiUrl.account.users, { ...queryParams }).pipe(
        map((response) => {
          response.items = response.items.map((user) => new ApplicationUser(user))
          return response;
        })
    )
  }

  getCurrentUser(): Observable<CurrentUser> {
    return super.get<CurrentUser>(apiUrl.account.currentUser);
  }
}
