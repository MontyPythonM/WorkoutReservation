import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';

@Component({
  selector: 'app-account-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.css']
})
export class AccountSettingsComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
  }

  // TODO: create account page

  ngOnInit(): void {
  }

}
