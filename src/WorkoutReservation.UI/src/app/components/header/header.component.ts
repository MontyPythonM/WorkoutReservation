import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/common/base.component';

@Component({
  selector: 'app-header',
  template: `
    <nav class="nav-menu">
      <app-logo class="page-logo" routerLink='/'></app-logo>
      <app-nav-bar class="buttons"></app-nav-bar>
    </nav>
  `,
  styleUrls: ['./header.component.css']
})
export class HeaderComponent extends BaseComponent {

  constructor() {
    super();
  }
}
