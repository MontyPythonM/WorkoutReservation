import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {UserService} from "../../../services/user.service";
import {PagedResult} from "../../../models/paged-result.model";
import {User} from "../../../models/user.model";
import {PagedQuery} from "../../../models/paged-query.model";
import DevExpress from "devextreme";
import dxDataGrid = DevExpress.ui.dxDataGrid;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent extends BaseComponent implements OnInit {

  users?: PagedResult<User>;
  pagerInfo: string;
  private grid: any;
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
    this.pagerInfo = "";
  }

  ngOnInit(): void {
    this.loadUsers(this.query);
  }

  loadUsers(query: PagedQuery) {
    this.subscribe(this.userService.getUsers(query), {
      next: (response: PagedResult<User>) => {
        this.users = response;
        this.pagerInfo = `Total items: ${ this.users?.totalItemsCount } | Items from ${ this.users?.itemsFrom } to ${ this.users?.itemsTo }`;
      }
    });
  }

  propertyChange(e: any) {
    if(e.fullName === "paging.pageSize") {
      this.query.pageSize = e.value
      this.loadUsers(this.query);
    }

    if(e.fullName === "paging.pageIndex") {
      this.query.pageNumber = e.value
      this.loadUsers(this.query);
    }
  }

  onGridInitialized = (e: {component: dxDataGrid}) => this.grid = e.component;
}
