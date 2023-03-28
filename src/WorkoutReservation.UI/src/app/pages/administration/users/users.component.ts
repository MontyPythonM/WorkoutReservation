import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {PagedResult} from "../../../models/paged-result.model";
import {PagedQuery} from "../../../models/paged-query.model";
import {ApplicationUser} from "../../../models/application-user.model";
import {UserService} from "../../../services/application-user.service";
import {DATETIME_FORMAT} from "../../../common/constants";
import {SortBySelector} from "../../../models/enums/sort-by-selector.enum";
import {Permission} from "../../../models/enums/permission.enum";
import {Row} from 'devextreme/ui/data_grid';
import {Role} from "../../../models/enums/role.enum";
import {UserAccount} from "../../../models/user-account.model";
import {AccountService} from "../../../services/identity/account.service";

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
  permissions = Permission;
  isDeletePopupVisible: boolean;
  userToDelete?: string;
  currentUserId?: string;

  constructor(private userService: UserService, private accountService: AccountService) {
    super();
    this.users = new PagedResult<ApplicationUser>();
    this.queryParams = new PagedQuery({
      pageNumber: 1,
      pageSize: 15,
      sortByDescending: false,
      sortBy: '',
      searchPhrase: ''
    });
    this.isDeletePopupVisible = false
  }

  ngOnInit(): void {
    this.loadUsers(this.queryParams);
    this.loadCurrentUser();
  }

  loadUsers(query: PagedQuery) {
    this.subscribe(this.userService.getUsers(query), {
      next: (response: PagedResult<ApplicationUser>) => {
        this.users = response;
      }
    });
  }

  loadCurrentUser() {
    this.subscribe(this.accountService.userAccount$, {
      next: (user: UserAccount) => this.currentUserId = user.id
    });
  }

  deleteApplicationUser = () => {
    this.subscribe(this.userService.deleteUser(this.userToDelete!), {
      next: () => {
        this.notificationService.show('Application user has been successfully deleted', 'success');
        this.ngOnInit();
      },
      error: () => this.notificationService.show('Failed to delete application user', 'error')
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

  openDeletePopup = (e: {row: Row}) => {
    this.userToDelete = e.row.data.id;
    this.isDeletePopupVisible = true;
  }

  cannotBeDeleted = (e: {row: Row}): boolean => e.row.data.id === this.currentUserId || e.row.data.isDeleted;

  searchPhraseChanged = (value: string) => this.queryParams.searchPhrase = value;
  sortByChanged = (value: string) => this.queryParams.sortBy = value;
  orderByChanged = (value: boolean) => this.queryParams.sortByDescending = value;
}
