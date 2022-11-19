export class InstructorDetailsCommand {
  firstName: string;
  lastName: string;
  gender: number;
  biography: string;
  email: string;

  constructor(firstName: string, lastName: string, gender: number, biography: string, email: string) {
    this.firstName =firstName,
    this.lastName = lastName,
    this.gender = gender,
    this.biography = biography,
    this.email = email
  }
}