export class WorkoutType {
  id: number;
  name: string;
  description: string;
  intensity: string;
  workoutTypeTags: string[];

  constructor(data: {id: number, name: string, description: string, intensity: string, workoutTypeTags: string[]}) {
    this.id = data.id;
    this.name = data.name;
    this.description = data.description;
    this.intensity = data.intensity;
    this.workoutTypeTags = data.workoutTypeTags;
  }
}