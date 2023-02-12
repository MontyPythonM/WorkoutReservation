export class UserAccount {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  gender: number;
  roles: string[];
  permissions: string[];

  constructor(id: string, email: string, firstName: string, lastName: string, gender: number, roles: string[], permissions: string[]) {
    this.id = id;
    this.email = email;
    this.firstName =firstName;
    this.lastName = lastName;
    this.gender = gender;
    this.roles = roles;
    this.permissions = permissions;
  }
}
