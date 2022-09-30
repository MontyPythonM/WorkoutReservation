import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-instructors',
  templateUrl: './instructors.component.html',
  styleUrls: ['./instructors.component.css']
})
export class InstructorsComponent implements OnInit {

  title: string = 'Instructors';
  instructors: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.getInstructors();
  }

  getInstructors() {
    this.httpClient.get('http://localhost:5001/api/instructor').subscribe({
      next: response => this.instructors = response,
      error: error => console.log(error)
    })
  }

}
