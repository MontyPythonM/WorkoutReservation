import {enumToObjects} from "./enum-converter";

export enum Gender {
  Unspecified,
  Female,
  Male
}

export const genders = enumToObjects(Gender);
