export class WorkoutType {
  id: number;
  name: string;
  description: string;
  intensity: string;
  workoutTypeTags: number[];
  instructors: number[];

  constructor(data: {id: number, name: string, description: string, intensity: string, workoutTypeTags: number[], instructors: number[]}) {
    this.id = data.id;
    this.name = data.name;
    this.description = data.description;
    this.intensity = data.intensity;
    this.workoutTypeTags = data.workoutTypeTags;
    this.instructors = data.instructors;
  }
}
