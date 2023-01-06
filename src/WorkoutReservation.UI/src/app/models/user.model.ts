export class User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  gender: string;
  accountCreationDate: string;
  userRole: string;

  constructor(data: {id: string, email: string, firstName: string, lastName: string, gender: string, accountCreationDate: string, userRole: string}) {
    this.id = data.id;
    this.email = data.email;
    this.firstName = data.firstName;
    this.lastName = data.lastName;
    this.gender = data.gender;
    this.accountCreationDate = data.accountCreationDate;
    this.userRole = data.userRole;
  }
}
