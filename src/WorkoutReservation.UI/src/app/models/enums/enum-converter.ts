export const enumToObjects = (enumType: any): EnumObject[] => {
  return Object.entries(enumType)
    .filter((entry: [any, any]) => !isNaN(entry[0]))
    .map((entry: [string, any]) => {
      return { index: parseInt(entry[0]), value: entry[1] };
    })
}

export interface EnumObject {
  index: number,
  value: string
}