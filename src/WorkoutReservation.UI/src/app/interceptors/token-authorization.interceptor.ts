import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserService} from "../services/user.service";
import {Injectable} from "@angular/core";

@Injectable()
export class TokenAuthorizationInterceptor implements HttpInterceptor {

  constructor(private userService: UserService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request.clone({
      setHeaders: {
        'Authorization': 'Bearer ' + this.userService.getToken()
      }
    }));
  }
}
