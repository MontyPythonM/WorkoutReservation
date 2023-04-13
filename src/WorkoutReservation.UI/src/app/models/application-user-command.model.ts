export class ApplicationUserCommand {
  id: string;
  roles: number[];

  constructor(id: string, roles: number[]) {
    this.id = id;
    this.roles = roles;
  }
}
