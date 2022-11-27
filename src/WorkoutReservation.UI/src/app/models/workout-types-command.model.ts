export class WorkoutTypeCommand {
  name: string;
  description: string;
  intensity: number;

  constructor(data: { name: string, description: string, intensity: number }) {
    this.name = data.name;
    this.description = data.description;
    this.intensity = data.intensity;
  }
}
