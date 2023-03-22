import {Component, OnInit} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {PagedQuery} from "../../models/paged-query.model";
import {PagedResult} from "../../models/paged-result.model";
import {Reservation} from "../../models/reservation.model";
import {ReservationService} from "../../services/reservation.service";
import {Router} from "@angular/router";
import dxDataGrid from "devextreme/ui/data_grid";
import {pageUrls} from "../../../environments/page-urls";
import {DATE_FORMAT} from "../../common/constants";

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent extends BaseComponent implements OnInit {
  reservations: PagedResult<Reservation>;
  queryParams: PagedQuery;
  dateFormat = DATE_FORMAT;
  isReservationsExist: boolean;
  private dxDataGrid?: dxDataGrid;

  constructor(private reservationService: ReservationService, private router: Router) {
    super();
    this.reservations = new PagedResult<Reservation>();
    this.queryParams = new PagedQuery({
      pageNumber: 1,
      pageSize: 20,
      sortByDescending: true,
      sortBy: 'WorkoutDate',
      searchPhrase: ''
    });
    this.isReservationsExist = true;
   }

  ngOnInit(): void {
    this.loadReservations(this.queryParams);
  }

  protected loadReservations(queryParams: PagedQuery): void {
    this.subscribe(this.reservationService.getOwnReservations(queryParams), {
      next: (response: PagedResult<Reservation>) => {
        this.reservations = response;
        if (response.totalItemsCount === 0 &&
          this.queryParams.searchPhrase == "") {
          this.isReservationsExist = false;
        }
      }
    });
  }

  pageSizeChanged(e: number) {
    this.queryParams.pageSize = e;
    this.loadReservations(this.queryParams);
  }

  pageNumberChanged(e: number) {
    this.queryParams.pageNumber = e;
    this.loadReservations(this.queryParams);
  }

  navigateToReservationDetails(e: any){
    this.router.navigateByUrl(pageUrls.reservations + e.data.id);
  }

  searchPhraseChanged = (value: string) => this.queryParams.searchPhrase = value;
  selectedFilterChanged = (value: string) => this.queryParams.sortBy = value;
  orderByChanged = (value: boolean) => this.queryParams.sortByDescending = value;

  refresh() {
    console.log(this.queryParams)
    this.ngOnInit();
  }

  onDataGridInit = (e: { component: dxDataGrid }) => this.dxDataGrid = e.component;
}
