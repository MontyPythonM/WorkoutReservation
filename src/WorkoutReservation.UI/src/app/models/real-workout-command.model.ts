export class RealWorkoutCommand {
  id?: number;
  maxParticipantNumber?: number;
  date?: string;
  startTime?: string;
  endTime?: string;
  instructorId?: number;
  workoutTypeId?: number;

  constructor(id?: number, maxParticipantNumber?: number, date?: string, startTime?: string, endTime?: string,
      instructorId?: number, workoutTypeId?: number) {
    this.id = id;
    this.maxParticipantNumber = maxParticipantNumber;
    this.date = date;
    this.startTime = startTime;
    this.endTime = endTime;
    this.instructorId = instructorId;
    this.workoutTypeId = workoutTypeId;
  }
}
