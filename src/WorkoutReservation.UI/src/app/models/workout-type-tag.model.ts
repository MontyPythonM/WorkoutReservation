export class WorkoutTypeTag {
  id?: number;
  tag?: string;
  isActive?: boolean;
  numberOfUses?: number;
  createdDate?: Date;
  createdBy?: string;

  constructor(data?: {id: number, tag: string, isActive: boolean, numberOfUses: number, createdDate: Date, createdBy: string}) {
    this.id = data?.id;
    this.tag = data?.tag;
    this.isActive = data?.isActive;
    this.numberOfUses = data?.numberOfUses;
    this.createdDate = data?.createdDate;
    this.createdBy = data?.createdBy;
  }
}
