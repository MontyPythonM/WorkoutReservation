export class RealWorkout {
  id: number;
  currentParticipantNumber: number;
  maxParticipantNumber: number;
  startDate: Date;
  endDate: Date;
  isExpired: boolean;
  isAlreadyReserved: boolean;
  reservationId: number | undefined;
  workoutTypeId: number;
  workoutTypeName: string;
  workoutIntensity: string;
  instructorId: number;
  instructorFullName: string;
  instructorShortName: string;

  constructor(data: { id: number, currentParticipantNumber: number, maxParticipantNumber: number,
    startDate: Date, endDate: Date, isExpired: boolean, isAlreadyReserved: boolean, reservationId: number | undefined,
    workoutTypeId: number, workoutTypeName: string, workoutIntensity: string, instructorId: number,
    instructorFullName: string, instructorShortName: string}) {
    this.id = data.id;
    this.currentParticipantNumber = data.currentParticipantNumber;
    this.maxParticipantNumber = data.maxParticipantNumber;
    this.startDate = data.startDate;
    this.endDate = data.endDate;
    this.isExpired = data.isExpired;
    this.reservationId = data.reservationId;
    this.isAlreadyReserved = data.isAlreadyReserved;
    this.workoutTypeId = data.workoutTypeId;
    this.workoutTypeName = data.workoutTypeName;
    this.workoutIntensity = data.workoutIntensity;
    this.instructorId = data.instructorId;
    this.instructorFullName = data.instructorFullName;
    this.instructorShortName = data.instructorShortName;
  }
}
