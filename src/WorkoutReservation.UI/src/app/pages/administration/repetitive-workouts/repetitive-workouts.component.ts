import { Component, OnInit } from '@angular/core';
import {BaseComponent} from "../../../common/base.component";

@Component({
  selector: 'app-repetitive-workouts',
  templateUrl: './repetitive-workouts.component.html',
  styleUrls: ['./repetitive-workouts.component.css']
})
export class RepetitiveWorkoutsComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
  }

  ngOnInit(): void {
  }
}
