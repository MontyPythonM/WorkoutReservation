import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";

@Component({
  selector: 'app-workout-type-tags',
  templateUrl: './workout-type-tags.component.html',
  styleUrls: ['./workout-type-tags.component.css']
})
export class WorkoutTypeTagsComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
  }

  ngOnInit(): void {
  }

}
