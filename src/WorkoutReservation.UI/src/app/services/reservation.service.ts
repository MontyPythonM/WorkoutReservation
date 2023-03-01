import {Injectable} from '@angular/core';
import {BaseService} from "../common/base.service";
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {PagedResult} from "../models/paged-result.model";
import {apiUrl} from "../../environments/api-urls";
import {Reservation} from "../models/reservation.model";
import {EditReservation} from "../models/edit-reservation.model";

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

  getAllReservations(queryParams: any): Observable<PagedResult<Reservation>> {
    return super.get<PagedResult<Reservation>>(apiUrl.reservation.getAll,
      { ...queryParams }).pipe(
      map((response) => {
        response.items = response.items.map((reservation) => new Reservation(reservation));
        return response;
      })
    );
  }

  getDetails(reservationId: number): Observable<Reservation> {
    return super.get<Reservation>(apiUrl.reservation.getDetails, { reservationId });
  }

  addReservation(realWorkoutId: number): Observable<void> {
    return super.post<void>(apiUrl.reservation.create, { realWorkoutId });
  }

  cancelReservation(reservationId: number) {
    return super.patch(apiUrl.reservation.cancel, { reservationId });
  }

  editReservation(reservation: EditReservation) {
    return super.patch(apiUrl.reservation.updateReservation, { ...reservation });
  }
}
