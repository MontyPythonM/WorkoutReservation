import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { map, Observable, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginForm } from '../models/login-form.model';
import { RegisterForm } from '../models/register-form.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private currentUserSource = new ReplaySubject<string>(1);
  private baseUrl = environment.apiUrl;
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, public datepipe: DatePipe) { }

  login(loginForm: LoginForm): Observable<string> {
    return this.http.post<any>(this.baseUrl + 'account/login', loginForm).pipe(
      map((token: string) => {
        return token;
      })
    )
  }

  register(registerForm: RegisterForm): Observable<string> {
    registerForm.dateOfBirth = this.datepipe.transform(registerForm.dateOfBirth, 'dd-MM-yyyy')!;
    return this.http.post<any>(this.baseUrl + 'account/register', registerForm);
  }
}