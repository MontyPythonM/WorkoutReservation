import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Injectable} from "@angular/core";
import {AccountService} from "../services/identity/account.service";
import {PermissionService} from "../services/identity/permission.service";

@Injectable()
export class TokenAuthorizationInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService,
              private permissionService: PermissionService) {}

  intercept = (request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> =>
    this.permissionService.isAuthenticated() ?
      next.handle(request.clone({setHeaders: {'Authorization': 'Bearer ' + this.accountService.getValidToken()}})) :
      next.handle(request);
}
