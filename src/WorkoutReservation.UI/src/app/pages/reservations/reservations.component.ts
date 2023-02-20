import {Component, OnInit} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {PagedQuery} from "../../models/paged-query.model";
import {PagedResult} from "../../models/paged-result.model";
import {Reservation} from "../../models/reservation.model";
import {EnumObject, enumToObjects} from "../../models/enums/enum-converter";
import {ReservationStatus} from "../../models/enums/reservation-status.enum";
import {ReservationService} from "../../services/reservation.service";
import {Router} from "@angular/router";
import {DATE_FORMAT} from "../../constants/constants";
import dxDataGrid from "devextreme/ui/data_grid";

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent extends BaseComponent implements OnInit {
  reservations: PagedResult<Reservation>;
  queryParams: PagedQuery;
  reservationStatus: EnumObject[];
  dateFormat = DATE_FORMAT;
  isInfoPopupOpen: boolean;
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
    this.reservationStatus = enumToObjects(ReservationStatus);
    this.isInfoPopupOpen = false;
   }

  ngOnInit(): void {
    this.loadReservations(this.queryParams);
  }

  protected loadReservations(queryParams: PagedQuery): void {
    this.subscribe(this.reservationService.getOwnReservations(queryParams), {
      next: (response: PagedResult<Reservation>) => this.reservations = response
    });
  }

  pageSizeChanged(e: any) {
    this.queryParams.pageSize = e;
    this.loadReservations(this.queryParams);
  }

  pageNumberChanged(e: any) {
    this.queryParams.pageNumber = e;
    this.loadReservations(this.queryParams);
  }

  navigateToReservationDetails(e: any){
    this.router.navigateByUrl('reservations/' + e.data.id);
  }

  openInfoPopup = () => this.isInfoPopupOpen = true;
  closeInfoPopup = () => this.isInfoPopupOpen = false;

  onDataGridInit = (e: { component: dxDataGrid }) => this.dxDataGrid = e.component;
}
