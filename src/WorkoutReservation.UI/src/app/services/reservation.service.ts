import {Injectable} from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {PagedResult} from "../models/paged-result.model";
import {apiUrl} from "../../environments/api-urls";
import {Reservation} from "../models/reservation.model";

@Injectable({
  providedIn: 'root'
})
export class ReservationService extends BaseService {

  constructor(protected override http: HttpClient) {
    super(http);
  }

  getOwnReservations(queryParams: any): Observable<PagedResult<Reservation>> {
    return super.get<PagedResult<Reservation>>(apiUrl.reservation.getOwn,
      { ...queryParams }).pipe(
      map((response) => {
        response.items = response.items.map((reservation) => new Reservation(reservation));
        return response;
      })
    );
  }

  getSomeoneReservations(queryParams: any, userId: string): Observable<PagedResult<Reservation>> {
    return super.get<PagedResult<Reservation>>(apiUrl.reservation.getSomeone,
      { ...queryParams, userId }).pipe(
      map((response) => {
        response.items = response.items.map((reservation) => new Reservation(reservation));
        return response;
      })
    );
  }
}
