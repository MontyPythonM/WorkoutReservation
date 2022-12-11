import {Instructor} from "./instructor.model";
import {WorkoutTypeTag} from "./workout-type-tag.model";

export class WorkoutType {
  id: number;
  name: string;
  description: string;
  intensity: string;
  workoutTypeTags: WorkoutTypeTag[];
  instructors: Instructor[];

  constructor(data: {id: number, name: string, description: string, intensity: string, workoutTypeTags: WorkoutTypeTag[], instructors: Instructor[]}) {
    this.id = data.id;
    this.name = data.name;
    this.description = data.description;
    this.intensity = data.intensity;
    this.workoutTypeTags = data.workoutTypeTags;
    this.instructors = data.instructors;
  }
}
