import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { ApiUrl } from "src/environments/api-urls";
import { environment } from "src/environments/environment";
import { BaseService } from "../common/base.service";
import { InstructorDetailsCommand } from "../models/instructor-details-command.model";
import { InstructorDetails } from "../models/instructor-details.model";
import { Instructor } from "../models/instructor.model";

@Injectable({
  providedIn: 'root'
})
export class InstructorService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getAll(): Observable<Instructor[]> {
    return this.http.get<Instructor[]>(environment.apiUrl + 'instructor').pipe(
      map((response) => {
        response = response.map((instructors) => new Instructor(instructors));
        return response;
      })
    )
  }

  getInstructorDetails(id: number): Observable<InstructorDetails> {
    return super.get<InstructorDetails>(ApiUrl.instructor + id);
  }

  deleteInstructor(id: number): Observable<void> {
    return super.delete<void>(ApiUrl.instructor + id);
  }

  createInstructor(instructor: InstructorDetailsCommand): Observable<void> {
    return super.post(ApiUrl.instructor, { ...instructor });
  }

  updateInstructor(instructorId: number, instructor: InstructorDetailsCommand): Observable<void> {
    return super.put(ApiUrl.instructor, { instructorId, ...instructor });
  }
}