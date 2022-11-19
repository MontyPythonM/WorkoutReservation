import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent extends BaseComponent{

  constructor() {
    super();
  }
}
