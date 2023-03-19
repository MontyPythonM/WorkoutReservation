export class ApplicationUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  gender: string;
  createdDate: string;
  userRoles: string[];

  constructor(data: {id: string, email: string, firstName: string, lastName: string, gender: string, createdDate: string, userRoles: string[]}) {
    this.id = data.id;
    this.email = data.email;
    this.firstName = data.firstName;
    this.lastName = data.lastName;
    this.gender = data.gender;
    this.createdDate = data.createdDate;
    this.userRoles = data.userRoles;
  }
}
