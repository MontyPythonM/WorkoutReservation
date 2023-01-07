import {DatePipe} from '@angular/common';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable, ReplaySubject} from 'rxjs';
import {environment} from 'src/environments/environment';
import {LoginForm} from '../models/login-form.model';
import {RegisterForm} from '../models/register-form.model';
import {BaseService} from "../common/base.service";
import {apiUrl} from "../../environments/api-urls";
import {User} from "../models/user.model";
import {PagedResult} from "../models/paged-result.model";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  private currentUserSource = new ReplaySubject<string>(1);
  private baseUrl = environment.apiUrl;
  currentUser$ = this.currentUserSource.asObservable();

  constructor(protected override http: HttpClient, public datepipe: DatePipe) {
    super(http);
  }

  login(loginForm: LoginForm): Observable<string> {
    return super.post<any>(apiUrl.account.login, loginForm).pipe(
      map((token: string) => {
        return token;
      })
    )
  }

  register(registerForm: RegisterForm): Observable<string> {
    registerForm.dateOfBirth = this.datepipe.transform(registerForm.dateOfBirth, 'dd-MM-yyyy')!;
    return super.post<any>(apiUrl.account.register, registerForm);
  }

  getUsers(queryParams: any): Observable<PagedResult<User>> {
    return super.get<PagedResult<User>>(apiUrl.account.users,
      { ...queryParams }).pipe(
        map((response) => {
          response.items = response.items.map((user) => new User(user))
          return response;
        })
      )
  }
}
