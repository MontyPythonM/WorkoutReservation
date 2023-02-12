import {Injectable} from '@angular/core';
import {BehaviorSubject, map, Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {BaseService} from "../common/base.service";
import {UserAccount} from "../models/user-account.model";
import {LoginForm} from "../models/interfaces/login-form.model";
import {apiUrl} from "../../environments/api-urls";
import {StorageKeys} from "../common/storage-keys.enum";
import {JwtHelperService} from "@auth0/angular-jwt";
import {RegisterForm} from "../models/register-form.model";
import {DatePipe} from "@angular/common";
import {Router} from "@angular/router";
import {NotificationService} from "./notification.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService extends BaseService {

  private userAccount$: BehaviorSubject<UserAccount>;

  constructor(protected override http: HttpClient,
              private datePipe: DatePipe,
              private router: Router,
              private notificationService: NotificationService) {
    super(http);
    this.userAccount$ = new BehaviorSubject<UserAccount>({} as UserAccount);
  }

  setUserAccount = (account: UserAccount) => this.userAccount$.next(account);

  hasPermission(permissions: string[]): Observable<boolean> {
    return this.userAccount$.pipe(
      map((currentUser: UserAccount) => permissions.some(p => currentUser.permissions.includes(p)))
    );
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

  getCurrentUser(): Observable<UserAccount> {
    return super.get<UserAccount>(apiUrl.account.currentUser);
  }

  public get isAuthenticated(): boolean {
    return !!sessionStorage.getItem(StorageKeys.Token);
  }

  register(registerForm: RegisterForm): Observable<string> {
    registerForm.dateOfBirth = this.datePipe.transform(registerForm.dateOfBirth, 'dd-MM-yyyy')!;
    return super.post<any>(apiUrl.account.register, registerForm);
  }
}
