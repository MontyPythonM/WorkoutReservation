import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { InstructorDetails } from "../models/instructor-details.model";
import { Instructor } from "../models/instructor.model";

@Injectable({
  providedIn: 'root'
})
export class InstructorService {

  constructor(private http: HttpClient) {}

  getAll(): Observable<Instructor[]> {
    return this.http.get<Instructor[]>(environment.apiUrl + 'instructor').pipe(
      map((response) => {
        response = response.map((instructors) => new Instructor(instructors));
        return response;
      })
    )
  }

  getDetails(id: number): Observable<InstructorDetails> {
    return this.http.get<InstructorDetails>(environment.apiUrl + 'instructor/' + id);
  }
}