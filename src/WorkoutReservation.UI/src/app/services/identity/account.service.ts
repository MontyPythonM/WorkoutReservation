import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {BehaviorSubject, map, Observable} from "rxjs";
import {Router} from "@angular/router";
import {NotificationService} from "../notification.service";
import {DatePipe} from "@angular/common";
import {apiUrl} from "../../../environments/api-urls";
import {LocalStorageService} from "./local-storage.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {LoginForm} from "../../models/interfaces/login-form.model";
import {UserAccount} from "../../models/user-account.model";
import {RegisterForm} from "../../models/register-form.model";
import {BaseService} from "../../common/base.service";
import {DATEONLY_FORMAT} from "../../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class AccountService extends BaseService {
  private userAccountSource = new BehaviorSubject<UserAccount>({} as UserAccount);
  private isLoggedSource = new BehaviorSubject<boolean>(false);
  public userAccount$ = this.userAccountSource.asObservable();
  public isLogged$ = this.isLoggedSource.asObservable();

  constructor(protected override http: HttpClient,
              private datePipe: DatePipe,
              private router: Router,
              private notificationService: NotificationService,
              private localStorageService: LocalStorageService) {
    super(http);
  }

  login(loginForm: LoginForm): Observable<void> {
    return super.post<string>(apiUrl.account.login, loginForm, {}, {responseType: 'text'})
      .pipe(map((token: string) => {
        this.localStorageService.set(token);
        this.setUserAccountOrDefault();
        this.router.navigateByUrl('/home');
      })
    );
  }

  logout(): void {
    if (this.localStorageService.get() !== null) {
      this.localStorageService.remove();
      this.setUserAccountOrDefault();
      this.router.navigateByUrl('/login');
      this.notificationService.show('Successfully logged out', 'success')
    }
  }

  register(registerForm: RegisterForm): Observable<string> {
    registerForm.dateOfBirth = this.datePipe.transform(registerForm.dateOfBirth, DATEONLY_FORMAT)!;
    return super.post<any>(apiUrl.account.register, registerForm);
  }

  getValidToken(): string | null {
    const token = this.localStorageService.get();
    if (token && new JwtHelperService().isTokenExpired(token)) {
      this.logout();
      return null;
    }
    return token;
  }

  setUserAccountOrDefault() {
    const token = this.localStorageService.get();
    if (token) {
      const decodedToken = new JwtHelperService().decodeToken(token);
      this.setCurrentUser(new UserAccount(
        decodedToken.sub,
        decodedToken.name,
        decodedToken.permissions));
      this.setLoggedIn(true);
    }
    else {
      this.setCurrentUser({} as UserAccount);
      this.setLoggedIn(false);
    }
  }

  private setCurrentUser = (account: UserAccount) => this.userAccountSource.next(account);
  private setLoggedIn = (loggedIn: boolean) => this.isLoggedSource.next(loggedIn);
}
