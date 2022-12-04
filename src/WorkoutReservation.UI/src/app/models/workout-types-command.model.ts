export class WorkoutTypeCommand {
  workoutTypeid?: number;
  name?: string;
  description?: string;
  intensity?: number;

  constructor(workoutTypeid?: number, name?: string, description?: string, intensity?: number) {
    this.workoutTypeid = workoutTypeid;
    this.name = name;
    this.description = description;
    this.intensity = intensity;
  }
}
