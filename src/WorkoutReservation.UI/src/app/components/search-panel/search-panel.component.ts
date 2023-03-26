import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
  styleUrls: ['./search-panel.component.css']
})
export class SearchPanelComponent implements OnInit {
  @Input() allowedSortBy?: string[];
  @Input() initialOrderBy: boolean;
  @Output() searchPhraseChanged = new EventEmitter<string>();
  @Output() sortByChanged = new EventEmitter<string>();
  @Output() orderByDescendingChanged = new EventEmitter<boolean>();
  @Output() refresh = new EventEmitter<boolean>();
  searchPhrase: string;
  sortBy: string;
  orderByDescending: boolean;

  constructor() {
    this.searchPhrase = "";
    this.sortBy = "";
    this.orderByDescending = true;
    this.initialOrderBy = true;
  }

  ngOnInit(): void {
    this.sortBy = this.allowedSortBy ? this.allowedSortBy[0] : "";
    this.orderByDescending = this.initialOrderBy;
  }

  onSearch = () => {
    this.searchPhraseChanged.emit(this.searchPhrase);
    this.sortByChanged.emit(this.sortBy);
    this.orderByDescendingChanged.emit(this.orderByDescending)
    this.refresh.emit();
  }

  setSortBy = (e: any) => e.value !== undefined ? this.sortBy = e.value : "";

  setOrderByIcon = (): string => this.orderByDescending ? "arrowup" : "arrowdown";

  toggleOrderBy = () => this.orderByDescending = !this.orderByDescending;
}

