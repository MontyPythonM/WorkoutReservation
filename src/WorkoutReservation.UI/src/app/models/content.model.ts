import {ContentType} from "./enums/content-type.enum";

export class Content {
  id: string;
  type: ContentType;
  value: string;

  constructor(id: string, type: ContentType, value: string) {
    this.id = id;
    this.type = type;
    this.value = value;
  }
}
