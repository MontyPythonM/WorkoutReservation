export class WorkoutType {
  id: number;
  name: string;
  description: string;
  intensity: string;

  constructor(data: {id: number, name: string, description: string, intensity: string}) {
    this.id = data.id;
    this.name = data.name;
    this.description = data.description;
    this.intensity = data.intensity;
  }
}