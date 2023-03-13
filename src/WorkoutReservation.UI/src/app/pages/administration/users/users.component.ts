import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {PagedResult} from "../../../models/paged-result.model";
import {PagedQuery} from "../../../models/paged-query.model";
import {ApplicationUser} from "../../../models/application-user.model";
import {UserService} from "../../../services/application-user.service";
import {DATETIME_FORMAT} from "../../../common/constants";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent extends BaseComponent implements OnInit {
  users: PagedResult<ApplicationUser>;
  query: PagedQuery;
  dateFormat = DATETIME_FORMAT;

  constructor(private userService: UserService) {
    super();
    this.users = new PagedResult<ApplicationUser>();
    this.query = new PagedQuery({
      pageNumber: 1,
      pageSize: 15,
      sortByDescending: false,
      sortBy: '',
      searchPhrase: ''
    });
  }

  ngOnInit(): void {
    this.loadUsers(this.query);
  }

  loadUsers(query: PagedQuery) {
    this.subscribe(this.userService.getUsers(query), {
      next: (response: PagedResult<ApplicationUser>) => {
        this.users = response;
      }
    });
  }

  pageSizeChanged(e: any) {
    this.query.pageSize = e;
    this.loadUsers(this.query);
  }

  pageNumberChanged(e: any) {
    this.query.pageNumber = e;
    this.loadUsers(this.query);
  }
}
