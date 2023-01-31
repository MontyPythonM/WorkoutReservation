import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {UserService} from "../../../services/user.service";
import {NotificationService} from "../../../services/notification.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends BaseComponent{
  isUserAuthenticated: boolean;

  constructor(private userService: UserService,
              private notificationService: NotificationService) {
    super();
    this.isUserAuthenticated = this.userService.isAuthenticated;
  }

  onLogout = () => {
    if (this.isUserAuthenticated) {
      this.userService.logout();
      this.notificationService.show("Successfully logged out", "success");
    }
  }
}
