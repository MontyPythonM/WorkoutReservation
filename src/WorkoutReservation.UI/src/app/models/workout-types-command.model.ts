export class WorkoutTypeCommand {
  id?: number;
  name?: string;
  description?: string;
  intensity?: number;
  workoutTypeTags?: number[];
  instructors?: number[];

  constructor(id?: number, name?: string, description?: string, intensity?: number, workoutTypeTags?: number[], instructors?: number[]) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.intensity = intensity;
    this.workoutTypeTags = workoutTypeTags;
    this.instructors = instructors;
  }
}
