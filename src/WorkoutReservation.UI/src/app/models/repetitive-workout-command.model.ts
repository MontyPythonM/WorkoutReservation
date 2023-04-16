import {DayOfWeek} from "./enums/day-of-week.enum";
import {WorkoutIntensity} from "./enums/workout-intensity.enum";

export class RepetitiveWorkoutCommand {
  id?: number;
  maxParticipantNumber?: number;
  startTime?: string;
  endTime?: string;
  dayOfWeek?: DayOfWeek;
  workoutTypeId?: number;
  instructorId?: number;

  constructor(id?: number, maxParticipantNumber?: number, startTime?: string, endTime?: string,
              dayOfWeek?: DayOfWeek, workoutTypeId?: number, instructorId?: number) {
    this.id = id;
    this.maxParticipantNumber = maxParticipantNumber;
    this.startTime = startTime;
    this.endTime = endTime;
    this.dayOfWeek = dayOfWeek;
    this.workoutTypeId = workoutTypeId;
    this.instructorId = instructorId;
  }
}
