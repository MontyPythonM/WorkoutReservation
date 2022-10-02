import { Component, OnInit } from '@angular/core';
import { Instructor } from 'src/app/models/instructor.model';
import { InstructorService } from 'src/app/services/instructor.service';

@Component({
  selector: 'app-instructors',
  templateUrl: './instructors.component.html',
  styleUrls: ['./instructors.component.css']
})
export class InstructorsComponent implements OnInit {
  instructors?: Instructor[];

  constructor(private instructorService: InstructorService) {
    this.instructors = new Array<Instructor>();
  }

  ngOnInit(): void {
    this.loadInstructors();
  }

  loadInstructors(): void {
    this.instructorService.getAll().subscribe(
      (instructors) => {
        this.instructors = instructors;
      }
    );
  }
}
