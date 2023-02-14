import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import {AccountService} from "./account.service";
import {PermissionService} from "./permission.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private accountService: AccountService,
              private permissionService: PermissionService,
              private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (!this.permissionService.isAuthenticated()) {
      return this.router.createUrlTree(['home'])
    }

    if (this.permissionService.hasPermission(route.data['permission'])) {
      return true;
    }

    return this.router.createUrlTree(['home'])
  }
}
