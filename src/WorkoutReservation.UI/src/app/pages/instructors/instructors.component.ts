import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';
import { Instructor } from 'src/app/models/instructor.model';
import { InstructorService } from 'src/app/services/instructor.service';

@Component({
  selector: 'app-instructors',
  templateUrl: './instructors.component.html',
  styleUrls: ['./instructors.component.css']
})
export class InstructorsComponent extends BaseComponent implements OnInit {
  instructors?: Instructor[];

  constructor(private instructorService: InstructorService) {
    super();
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
