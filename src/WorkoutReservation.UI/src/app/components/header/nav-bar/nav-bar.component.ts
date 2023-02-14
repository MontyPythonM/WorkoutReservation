import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {PermissionService} from "../../../services/identity/permission.service";
import {AccountService} from "../../../services/identity/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends BaseComponent{

  constructor(private accountService: AccountService,
              private permissionService: PermissionService) {
    super();
  }

  onLogout = () => this.accountService.logout();
}
