import {enumToObjects} from "./enum-converter";

export enum WorkoutIntensity {
  Low = 1,
  Moderate,
  Vigorous,
  Extreme
}

export const workoutIntensities = enumToObjects(WorkoutIntensity);
