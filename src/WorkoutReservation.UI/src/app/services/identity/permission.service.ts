import {Injectable} from '@angular/core';
import {AccountService} from "./account.service";
import {map, Observable} from "rxjs";
import {UserAccount} from "../../models/user-account.model";
import {BaseService} from "../../common/base.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PermissionService extends BaseService {

  constructor(protected override http: HttpClient,
              private accountService: AccountService) {
    super(http);
  }

  hasPermissions(permissions: string | Array<string>): Observable<boolean> {
    return this.accountService.userAccount$.pipe(
      map((user: UserAccount) => {
        if (!this.hasValidToken() || !user.permissions) return false;
        return permissions instanceof Array ?
          permissions.some(p => user.permissions.includes(p)) :
          user.permissions.includes(permissions);
      }
    ));
  }

  hasValidToken = (): boolean => this.accountService.getValidToken() != null;
}
