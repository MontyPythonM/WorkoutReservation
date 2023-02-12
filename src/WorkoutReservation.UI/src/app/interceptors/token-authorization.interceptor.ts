import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Injectable} from "@angular/core";
import {AccountService} from "../services/account.service";

@Injectable()
export class TokenAuthorizationInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request.clone({
      setHeaders: {
        'Authorization': 'Bearer ' + this.accountService.getToken()
      }
    }));
  }
}
