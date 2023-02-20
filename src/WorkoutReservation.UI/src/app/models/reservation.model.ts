import {ReservationStatus} from "./enums/reservation-status.enum";
import {WorkoutIntensity} from "./enums/workout-intensity.enum";

export class Reservation {
  id: number;
  creationDate: Date;
  lastModificationDate: Date;
  reservationStatus: ReservationStatus;
  realWorkoutId: number;
  maxParticipantNumber: number;
  currentParticipantNumber: number;
  startTime: Date;
  endTime: Date;
  date: Date;
  workoutTypeId: number;
  workoutTypeName: string;
  intensity: WorkoutIntensity;
  instructorId: number;
  instructorFullName: string;

  constructor(data: { id: number, creationDate: Date, lastModificationDate: Date, reservationStatus: ReservationStatus,
    realWorkoutId: number, maxParticipantNumber: number, currentParticipantNumber: number, startTime: Date,
    endTime: Date, date: Date, workoutTypeId: number, workoutTypeName: string, intensity: WorkoutIntensity,
    instructorId: number, instructorFullName: string }) {
    this.id = data.id;
    this.creationDate = data.creationDate;
    this.lastModificationDate = data.lastModificationDate;
    this.reservationStatus = data.reservationStatus;
    this.realWorkoutId = data.realWorkoutId;
    this.maxParticipantNumber = data.maxParticipantNumber;
    this.currentParticipantNumber = data.currentParticipantNumber;
    this.startTime = data.startTime;
    this.endTime = data.endTime;
    this.date = data.date;
    this.workoutTypeId = data.workoutTypeId;
    this.workoutTypeName = data.workoutTypeName;
    this.intensity = data.intensity;
    this.instructorId = data.instructorId;
    this.instructorFullName = data.instructorFullName;
  }
}
