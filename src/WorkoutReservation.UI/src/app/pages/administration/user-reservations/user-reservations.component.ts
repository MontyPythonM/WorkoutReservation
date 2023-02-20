import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {AccountService} from "../../../services/identity/account.service";

@Component({
  selector: 'app-user-reservations',
  templateUrl: './user-reservations.component.html',
  styleUrls: ['./user-reservations.component.css']
})
export class UserReservationsComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
  }

  ngOnInit(): void {
  }
}
