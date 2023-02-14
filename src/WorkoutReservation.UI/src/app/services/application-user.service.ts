import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {map, Observable} from 'rxjs';
import {BaseService} from "../common/base.service";
import {apiUrl} from "../../environments/api-urls";
import {PagedResult} from "../models/paged-result.model";
import {ApplicationUser} from "../models/application-user.model";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getUsers(queryParams: any): Observable<PagedResult<ApplicationUser>> {
    return super.get<PagedResult<ApplicationUser>>(apiUrl.user.users, { ...queryParams }).pipe(
        map((response) => {
          response.items = response.items.map((user) => new ApplicationUser(user))
          return response;
        })
    );
  }
}
