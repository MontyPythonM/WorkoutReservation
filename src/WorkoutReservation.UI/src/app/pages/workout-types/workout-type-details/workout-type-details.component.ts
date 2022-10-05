import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';

@Component({
  selector: 'app-workout-type-details',
  templateUrl: './workout-type-details.component.html',
  styleUrls: ['./workout-type-details.component.css']
})
export class WorkoutTypeDetailsComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
  }

  ngOnInit(): void {
  }

}
