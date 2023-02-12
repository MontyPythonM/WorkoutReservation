import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {AccountService} from "../../../services/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends BaseComponent{
  isUserAuthenticated: boolean;

  constructor(private accountService: AccountService) {
    super();
    this.isUserAuthenticated = this.accountService.isAuthenticated;
  }

  onLogout = () => this.accountService.logout();
}
