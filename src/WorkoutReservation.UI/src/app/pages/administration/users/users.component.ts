import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {PagedResult} from "../../../models/paged-result.model";
import {PagedQuery} from "../../../models/paged-query.model";
import {ApplicationUser} from "../../../models/application-user.model";
import {UserService} from "../../../services/application-user.service";
import {DATETIME_FORMAT} from "../../../common/constants";
import {SortBySelector} from "../../../models/enums/sort-by-selector.enum";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent extends BaseComponent implements OnInit {
  users: PagedResult<ApplicationUser>;
  queryParams: PagedQuery;
  dateFormat = DATETIME_FORMAT;
  sortBy = SortBySelector;

  constructor(private userService: UserService) {
    super();
    this.users = new PagedResult<ApplicationUser>();
    this.queryParams = new PagedQuery({
      pageNumber: 1,
      pageSize: 15,
      sortByDescending: false,
      sortBy: '',
      searchPhrase: ''
    });
  }

  ngOnInit(): void {
    this.loadUsers(this.queryParams);
  }

  loadUsers(query: PagedQuery) {
    this.subscribe(this.userService.getUsers(query), {
      next: (response: PagedResult<ApplicationUser>) => {
        this.users = response;
      }
    });
  }

  pageSizeChanged(e: any) {
    this.queryParams.pageSize = e;
    this.loadUsers(this.queryParams);
  }

  pageNumberChanged(e: any) {
    this.queryParams.pageNumber = e;
    this.loadUsers(this.queryParams);
  }

  searchPhraseChanged = (value: string) => this.queryParams.searchPhrase = value;
  sortByChanged = (value: string) => this.queryParams.sortBy = value;
  orderByChanged = (value: boolean) => this.queryParams.sortByDescending = value;
}
