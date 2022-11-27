import { Injectable } from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class RepetitiveWorkoutService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }
}
