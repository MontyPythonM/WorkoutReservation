import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {BaseService} from "../common/base.service";
import {Content} from "../models/content.model";
import {apiUrl} from "../../environments/api-urls";

@Injectable({
  providedIn: 'root'
})
export class ContentService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getHomePage(): Observable<Content> {
    return super.get<Content>(apiUrl.content.homePage);
  }

  createHomePage(htmlContent: string): Observable<void> {
    return super.post<void>(apiUrl.content.homePage, { htmlContent });
  }
}
