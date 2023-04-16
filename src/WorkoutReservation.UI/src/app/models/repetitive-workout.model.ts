import {DayOfWeek} from "./enums/day-of-week.enum";
import {WorkoutIntensity} from "./enums/workout-intensity.enum";

export class RepetitiveWorkout {
  id: number;
  maxParticipantNumber: number;
  startDate: Date;
  endDate: Date;
  dayOfWeek: DayOfWeek;
  createdBy: string;
  createdDate: Date;
  lastModifiedBy: string;
  lastModifiedDate: Date;
  workoutTypeId: number;
  workoutTypeName: string;
  workoutIntensity: WorkoutIntensity;
  instructorId: number;
  instructorFullName: string;
  instructorEmail: string;

  constructor(data: { id: number, maxParticipantNumber: number, startDate: Date, endDate: Date, dayOfWeek: DayOfWeek,
    createdBy: string, createdDate: Date, lastModifiedBy: string, lastModifiedDate: Date, workoutTypeId: number,
    workoutTypeName: string, workoutIntensity: WorkoutIntensity, instructorId: number, instructorFullName: string,
    instructorEmail: string}) {
    this.id = data.id;
    this.maxParticipantNumber = data.maxParticipantNumber;
    this.startDate = data.startDate;
    this.endDate = data.endDate;
    this.dayOfWeek = data.dayOfWeek;
    this.createdBy = data.createdBy;
    this.createdDate = data.createdDate;
    this.lastModifiedBy = data.lastModifiedBy;
    this.lastModifiedDate = data.lastModifiedDate;
    this.workoutTypeId = data.workoutTypeId;
    this.workoutTypeName = data.workoutTypeName;
    this.workoutIntensity = data.workoutIntensity;
    this.instructorId = data.instructorId;
    this.instructorFullName = data.instructorFullName;
    this.instructorEmail = data.instructorEmail;
  }
}
