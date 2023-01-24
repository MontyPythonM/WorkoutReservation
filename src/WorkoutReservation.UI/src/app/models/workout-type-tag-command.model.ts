export class WorkoutTypeTagCommand {
  id?: number;
  tag?: string;
  isActive?: boolean;

  constructor(id?: number, tag?: string, isActive?: boolean) {
    this.id = id;
    this.tag = tag;
    this.isActive = isActive;
  }
}
