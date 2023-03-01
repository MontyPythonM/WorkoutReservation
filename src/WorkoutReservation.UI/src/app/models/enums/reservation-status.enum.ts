import {enumToObjects} from "./enum-converter";

export enum ReservationStatus {
  Reserved = 1,
  Cancelled,
  Absence,
  Presence
}

export const reservationStatuses = enumToObjects(ReservationStatus);
