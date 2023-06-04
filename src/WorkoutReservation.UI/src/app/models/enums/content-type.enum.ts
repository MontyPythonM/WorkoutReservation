import {enumToObjects} from "./enum-converter";

export enum ContentType {
  Undefined,
  HomePageHtml
}

export const contentType = enumToObjects(ContentType);
