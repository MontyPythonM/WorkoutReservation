export class PagedQuery {
  pageNumber: number;
  pageSize: number;
  sortByDescending: boolean;
  sortBy: string;
  searchPhrase: string;

  constructor(data: PagedQuery) {
    this.pageNumber = data.pageNumber,
    this.pageSize = data.pageSize,
    this.sortByDescending = data.sortByDescending,
    this.sortBy = data.sortBy,
    this.searchPhrase = data.searchPhrase
  }

  static default(): PagedQuery {
    return new PagedQuery({
      pageNumber: 1,
      pageSize: 5,
      sortByDescending: false,
      sortBy: '',
      searchPhrase: ''
    })
  }
}