import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {map, Observable} from "rxjs";
import {PermissionService} from "./permission.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private permissionService: PermissionService,
              private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (!this.permissionService.isAuthenticated()) {
      return this.router.createUrlTree(['home'])
    }

    if (route.data['permission'] == undefined) {
      return true;
    }

    return this.permissionService.hasPermissions(route.data['permission'])
      .pipe(map((result: boolean) => {
        return result ? true : this.router.createUrlTree(['home'])
      }
    ));
  }
}
