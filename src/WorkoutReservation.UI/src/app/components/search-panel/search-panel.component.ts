import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
  styleUrls: ['./search-panel.component.css']
})
export class SearchPanelComponent implements OnInit {
  @Input() allowedFilters?: string[];
  @Output() searchPhraseChanged = new EventEmitter<string>();
  @Output() filterByChanged = new EventEmitter<string>();
  @Output() orderByDescendingChanged = new EventEmitter<boolean>();
  @Output() refresh = new EventEmitter<boolean>();
  searchPhrase: string;
  filterBy: string;
  orderByDescending: boolean;

  constructor() {
    this.searchPhrase = "";
    this.filterBy = "";
    this.orderByDescending = false;
  }

  ngOnInit(): void {
    this.filterBy = this.allowedFilters ? this.allowedFilters[0] : "";
  }

  onSearch = () => {
    this.searchPhraseChanged.emit(this.searchPhrase);
    this.filterByChanged.emit(this.filterBy);
    this.orderByDescendingChanged.emit(this.orderByDescending)
    this.refresh.emit();
  }

  filterChanged = (e: any) => e.value !== undefined ? this.filterBy = e.value : "";

  setOrderByIcon = (): string => this.orderByDescending ? "arrowup" : "arrowdown";

  toggleOrderBy = () => this.orderByDescending = !this.orderByDescending;
}

