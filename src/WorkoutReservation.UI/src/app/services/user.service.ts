import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { map, Observable, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginForm } from '../models/login-form.model';
import { RegisterForm } from '../models/register-form.model';
import {BaseService} from "../common/base.service";
import {ApiUrl} from "../../environments/api-urls";

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
    return super.post<any>(ApiUrl.account.login, loginForm).pipe(
      map((token: string) => {
        return token;
      })
    )
  }

  register(registerForm: RegisterForm): Observable<string> {
    registerForm.dateOfBirth = this.datepipe.transform(registerForm.dateOfBirth, 'dd-MM-yyyy')!;
    return super.post<any>(ApiUrl.account.register, registerForm);
  }
}
