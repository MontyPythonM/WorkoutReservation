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

  users?: PagedResult<User>;
  query: PagedQuery;

  constructor(private userService: UserService) {
    super();
    this.users = new PagedResult<User>();
    this.query = PagedQuery.default();
  }

  ngOnInit(): void {
    this.loadUsers(this.query);
  }

  loadUsers(query: PagedQuery) {
    this.subscribe(this.userService.getUsers(query), {
      next: (response: PagedResult<User>) => this.users = response
    });
  }
}
