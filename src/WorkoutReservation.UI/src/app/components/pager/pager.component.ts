import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent implements OnInit {
  @Input() allowedPageSize: number[] = [];
  @Input() totalPages?: number;
  @Output() pageSizeChanged = new EventEmitter<number>();
  @Output() pageNumberChanged = new EventEmitter<number>();

  currentPageNumber: number;
  currentPageSize?: number;

  constructor() {
    this.currentPageNumber = 1;
  }

  ngOnInit(): void {
    this.currentPageSize = this.allowedPageSize[0];
  }

  onPageSizeChanged = (pageSize: number) => {
    this.currentPageSize = pageSize;
    this.pageSizeChanged.emit(pageSize);
  }

  private onPageNumberChanged = (pageNumber: number) => this.pageNumberChanged.emit(pageNumber);

  nextPage = () => {
    if (this.currentPageNumber < this.totalPages!) {
      this.currentPageNumber++;
      this.onPageNumberChanged(this.currentPageNumber);
    }
  }

  previousPage = () => {
    if (this.currentPageNumber > 1) {
      this.currentPageNumber--;
      this.onPageNumberChanged(this.currentPageNumber);
    }
  }
}