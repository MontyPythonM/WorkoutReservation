import {Injectable} from '@angular/core';
import {AccountService} from "./account.service";
import {map, Observable} from "rxjs";
import {UserAccount} from "../../models/user-account.model";

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor(private accountService: AccountService) {}

  hasPermission (permission: string): Observable<boolean> {
    return this.accountService.userAccount$.pipe(
      map((user: UserAccount) =>
        user.permissions.includes(permission)
    ));
  }

  isAuthenticated = () => this.accountService.getValidToken() != null;
}
