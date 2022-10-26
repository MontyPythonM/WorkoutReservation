import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/common/base.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent extends BaseComponent {
  pages: PageRoute[];
  routerLink: any;

  constructor(private router: Router) {
    super();
    this.pages = [
      { name: 'Home', path: '/home' },
      { name: 'Workouts', path: '/workouts' },
      { name: 'Instructors', path: '/instructors' },
      { name: 'Workout Types', path: '/workout-types' },
      { name: 'Register', path: '/register' },
      { name: 'Login', path: '/login' }
    ],
    this.routerLink = '/home';
  }

  setPage(event: any) {
    this.router.navigate([event.itemData.path]);
  }
}

interface PageRoute {
  name: string,
  path: string
}