import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-workout-types',
  templateUrl: './workout-types.component.html',
  styleUrls: ['./workout-types.component.css']
})
export class WorkoutTypesComponent implements OnInit {

  title: string = 'Workout types';
  workoutTypes: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.getWorkoutTypes();
  }

  getWorkoutTypes() {
    this.httpClient.get('http://localhost:5001/api/workout-type').subscribe(response => {
      this.workoutTypes = response;
    }, error => {
      console.log(error);
    })
  }

}
