export class Instructor {
  id?: number;
  firstName?: string;
  lastName?: string;

  constructor(data?: { id: number, firstName: string, lastName: string }) {
    this.id = data?.id;
    this.firstName = data?.firstName;
    this.lastName =  data?.lastName;
  }
}
