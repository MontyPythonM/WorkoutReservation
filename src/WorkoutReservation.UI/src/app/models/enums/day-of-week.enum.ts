import {enumToObjects} from "./enum-converter";
import {Gender} from "./gender.enum";

export enum DayOfWeek {
  Sunday,
  Monday,
  Tuesday,
  Wednesday,
  Thursday,
  Friday,
  Saturday,
}

export const daysOfWeek = enumToObjects(DayOfWeek);
