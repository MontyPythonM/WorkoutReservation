import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstructorDetails } from 'src/app/models/instructor-details.model';
import { InstructorService } from 'src/app/services/instructor.service';

@Component({
  selector: 'app-instructor-details',
  templateUrl: './instructor-details.component.html',
  styleUrls: ['./instructor-details.component.css']
})
export class InstructorDetailsComponent implements OnInit {
  instructor?: InstructorDetails;

  constructor(private instructorService: InstructorService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.loadInstructorDetails(this.route.snapshot.params['id']);
  }

  loadInstructorDetails(id: number): void {
    this.instructorService.getDetails(id).subscribe(
      (instructor) => {
        this.instructor = instructor;
      }
    );
  }
}
