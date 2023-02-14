export class UserAccount {
  id: string;
  fullName: string;
  permissions: string[];

  constructor(id: string, fullName: string, permissions: string[]) {
    this.id = id;
    this.fullName = fullName;
    this.permissions = permissions;
  }
}
