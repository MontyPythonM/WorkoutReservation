export class ApplicationUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  gender: string;
  createdDate: string;
  lastModifiedDate: string;
  isDeleted: boolean;
  roles: number[];

  constructor(data: {id: string, email: string, firstName: string, lastName: string, gender: string,
    createdDate: string, lastModifiedDate: string, isDeleted: boolean, roles: number[]}) {
    this.id = data.id;
    this.email = data.email;
    this.firstName = data.firstName;
    this.lastName = data.lastName;
    this.gender = data.gender;
    this.createdDate = data.createdDate;
    this.lastModifiedDate = data.lastModifiedDate;
    this.isDeleted = data.isDeleted;
    this.roles = data.roles;
  }
}
