import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable} from 'rxjs';
import {BaseService} from "../common/base.service";
import {apiUrl} from "../../environments/api-urls";
import {PagedResult} from "../models/paged-result.model";
import {ApplicationUser} from "../models/application-user.model";
import {Role} from "../models/enums/role.enum";
import {ApplicationUserCommand} from "../models/application-user-command.model";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getUsers(queryParams: any): Observable<PagedResult<ApplicationUser>> {
    return super.get<PagedResult<ApplicationUser>>(apiUrl.user.getUsers, { ...queryParams }).pipe(
        map((response) => {
          response.items = response.items.map((user) => new ApplicationUser(user))
          return response;
        })
    );
  }

  setUserRoles(userCommand: ApplicationUserCommand): Observable<void> {
    return super.patch(apiUrl.user.setUserRoles, { ...userCommand })
  }

  deleteUser(id: string): Observable<void> {
    return super.delete(apiUrl.user.deleteUser, { id });
  }
}
