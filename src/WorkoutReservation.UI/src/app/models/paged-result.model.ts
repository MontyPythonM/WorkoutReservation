export class PagedResult<T> {
  totalPages: number = 1;
  itemsFrom: number = 1;
  itemsTo: number = 5;
  totalItemsCount: number = 0;
  items: T[] = [];
}