import {DayOfWeek} from "./enums/day-of-week.enum";
import {WorkoutIntensity} from "./enums/workout-intensity.enum";

export class RepetitiveWorkoutCommand {
  id?: number;
  maxParticipantNumber?: number;
  startDate?: Date;
  endDate?: Date;
  dayOfWeek?: DayOfWeek;
  workoutTypeId?: number;
  instructorId?: number;

  constructor(id?: number, maxParticipantNumber?: number, startDate?: Date, endDate?: Date,
              dayOfWeek?: DayOfWeek, workoutTypeId?: number, instructorId?: number) {
    this.id = id;
    this.maxParticipantNumber = maxParticipantNumber;
    this.startDate = startDate;
    this.endDate = endDate;
    this.dayOfWeek = dayOfWeek;
    this.workoutTypeId = workoutTypeId;
    this.instructorId = instructorId;
  }
}
