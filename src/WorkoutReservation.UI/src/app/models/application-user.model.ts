export class ApplicationUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  gender: string;
  accountCreationDate: string;
  userRoles: string[];

  constructor(data: {id: string, email: string, firstName: string, lastName: string, gender: string, accountCreationDate: string, userRoles: string[]}) {
    this.id = data.id;
    this.email = data.email;
    this.firstName = data.firstName;
    this.lastName = data.lastName;
    this.gender = data.gender;
    this.accountCreationDate = data.accountCreationDate;
    this.userRoles = data.userRoles;
  }
}
