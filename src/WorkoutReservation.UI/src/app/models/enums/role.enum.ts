import {enumToObjects} from "./enum-converter";

export enum Role {
  SystemAdministrator = 1,
  BusinessAdministrator,
  Manager,
  Member
}

export const roles = enumToObjects(Role);
