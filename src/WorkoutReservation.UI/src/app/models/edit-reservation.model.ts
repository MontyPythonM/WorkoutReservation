import {ReservationStatus} from "./enums/reservation-status.enum";

export class EditReservation {
  reservationId: number;
  reservationStatus: ReservationStatus;
  note: string;

  constructor(reservationId: number, reservationStatus: ReservationStatus, note: string) {
    this.reservationId = reservationId;
    this.reservationStatus = reservationStatus;
    this.note = note ?? "";
  }
}
