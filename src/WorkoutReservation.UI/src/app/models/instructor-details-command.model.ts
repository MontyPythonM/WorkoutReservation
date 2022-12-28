export class InstructorDetailsCommand {
  id?: number;
  firstName?: string;
  lastName?: string;
  gender?: number;
  biography?: string;
  email?: string;

  constructor(id?: number, firstName?: string, lastName?: string, gender?: number, biography?: string, email?: string) {
    this.id = id;
    this.firstName =firstName;
    this.lastName = lastName;
    this.gender = gender;
    this.biography = biography;
    this.email = email;
  }
}
