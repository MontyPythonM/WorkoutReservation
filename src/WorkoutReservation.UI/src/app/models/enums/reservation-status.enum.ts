import {enumToObjects} from "./enum-converter";

export enum ReservationStatus {
  Reserved = 1,
  Cancelled
}

export const reservationStatuses = enumToObjects(ReservationStatus);
