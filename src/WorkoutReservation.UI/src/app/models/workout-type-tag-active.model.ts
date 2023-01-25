export class WorkoutTypeTagActive {
  id: number;
  tag: string;
  isActive: boolean;

  constructor(data: {id: number, tag: string, isActive: boolean}) {
    this.id = data.id;
    this.tag = data.tag;
    this.isActive = data.isActive;
  }
}
