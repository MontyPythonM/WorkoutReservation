import {WorkoutTypeTag} from "./workout-type-tag.model";

export class WorkoutTypeCommand {
  id?: number;
  name?: string;
  description?: string;
  intensity?: number;
  workoutTypeTags?: WorkoutTypeTag[];

  constructor(id?: number, name?: string, description?: string, intensity?: number, workoutTypeTags?: WorkoutTypeTag[]) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.intensity = intensity;
    this.workoutTypeTags = workoutTypeTags;
  }
}
