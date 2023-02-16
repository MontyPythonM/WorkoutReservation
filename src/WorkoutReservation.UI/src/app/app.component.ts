import {Component, OnInit} from '@angular/core';
import {AccountService} from "./services/identity/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.setUserAccountOrDefault();
  }
}
