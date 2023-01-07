import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {apiUrl} from "src/environments/api-urls";
import {BaseService} from "../common/base.service";
import {InstructorDetailsCommand} from "../models/instructor-details-command.model";
import {InstructorDetails} from "../models/instructor-details.model";
import {Instructor} from "../models/instructor.model";

@Injectable({
  providedIn: 'root'
})
export class InstructorService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getAll(): Observable<Instructor[]> {
    return super.get<Instructor[]>(apiUrl.instructor);
  }

  getDetails(id: number): Observable<InstructorDetails> {
    return super.get<InstructorDetails>(apiUrl.instructor + id);
  }

  remove(id: number): Observable<void> {
    return super.delete<void>(apiUrl.instructor + id);
  }

  create(instructor: InstructorDetailsCommand): Observable<void> {
    return super.post(apiUrl.instructor, { ...instructor });
  }

  update(instructor: InstructorDetailsCommand): Observable<void> {
    return super.put(apiUrl.instructor, { ...instructor });
  }
}
