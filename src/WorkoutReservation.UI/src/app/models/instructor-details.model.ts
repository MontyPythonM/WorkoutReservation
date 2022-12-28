export class InstructorDetails {
  id: number;
  firstName: string;
  lastName: string;
  gender: string;
  biography: string;
  email: string;
  workoutTypes: { id: number, name: string }[]

  constructor(data: { id: number, firstName: string, lastName: string, gender: string, biography: string, email: string, workoutTypes: { id: number, name: string }[] }) {
    this.id = data.id;
    this.firstName = data.firstName;
    this.lastName = data.lastName;
    this.gender = data.gender;
    this.biography = data.biography;
    this.email = data.email;
    this.workoutTypes = data.workoutTypes;
  }
}
