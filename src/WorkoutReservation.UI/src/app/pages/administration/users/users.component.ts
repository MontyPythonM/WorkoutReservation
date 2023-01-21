import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {UserService} from "../../../services/user.service";
import {PagedResult} from "../../../models/paged-result.model";
import {User} from "../../../models/user.model";
import {PagedQuery} from "../../../models/paged-query.model";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent extends BaseComponent implements OnInit {
  users: PagedResult<User>;
  private query: PagedQuery;

  constructor(private userService: UserService) {
    super();
    this.users = new PagedResult<User>();
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
      next: (response: PagedResult<User>) => {
        this.users = response;
        this.validPageSize();
      }
    });
  }

  validPageSize() {
    if (this.query.pageNumber > this.users.totalPages) {
      this.query.pageNumber = this.users.totalPages;
      this.loadUsers(this.query);
    }
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
