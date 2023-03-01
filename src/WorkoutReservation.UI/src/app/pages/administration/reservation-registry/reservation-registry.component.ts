import {Component, OnInit} from '@angular/core';
import {PagedResult} from "../../../models/paged-result.model";
import {Reservation} from "../../../models/reservation.model";
import {PagedQuery} from "../../../models/paged-query.model";
import {DATE_FORMAT} from "../../../constants/constants";
import {BaseComponent} from "../../../common/base.component";
import {ReservationService} from "../../../services/reservation.service";
import dxDataGrid, {Row} from "devextreme/ui/data_grid";
import {Permission} from "../../../models/enums/permission.enum";
import {EditReservation} from "../../../models/edit-reservation.model";
import dxForm from "devextreme/ui/form";
import {reservationStatuses} from "../../../models/enums/reservation-status.enum";

@Component({
  selector: 'app-reservation-registry',
  templateUrl: './reservation-registry.component.html',
  styleUrls: ['./reservation-registry.component.css']
})
export class ReservationRegistryComponent extends BaseComponent implements OnInit {
  reservations: PagedResult<Reservation>;
  queryParams: PagedQuery;
  dateFormat = DATE_FORMAT;
  reservationStatuses = reservationStatuses;
  permissions = Permission;
  editPopupVisible: boolean;
  reservationCommand?: EditReservation;
  private editPopupForm?: dxForm;
  private dxDataGrid?: dxDataGrid;

  constructor(private reservationService: ReservationService) {
    super();
    this.reservations = new PagedResult<Reservation>();
    this.queryParams = new PagedQuery({
      pageNumber: 1,
      pageSize: 20,
      sortByDescending: true,
      sortBy: 'WorkoutDate',
      searchPhrase: ''
    });
    this.editPopupVisible = false;
  }

  ngOnInit(): void {
    this.loadReservations(this.queryParams);
  }

  protected loadReservations(queryParams: PagedQuery): void {
    this.subscribe(this.reservationService.getAllReservations(queryParams), {
      next: (response: PagedResult<Reservation>) => this.reservations = response
    });
  }

  updateReservation() {
    if (!this.editPopupForm?.validate().isValid) return;
    this.subscribe(this.reservationService.editReservation(this.reservationCommand!), {
      next: () => {
        this.closeEditPopup();
        this.ngOnInit();
        this.notificationService.show("Reservation update successfully", "success");
      }
    });
  }

  openEditPopup = (e: {row: Row}) => {
    const row = e.row.data;
    this.reservationCommand = new EditReservation(row.id, row.reservationStatus, row.note);
    this.editPopupVisible = true;
  }

  closeEditPopup = () => this.editPopupVisible = false;

  pageSizeChanged(e: any) {
    this.queryParams.pageSize = e;
    this.loadReservations(this.queryParams);
  }

  pageNumberChanged(e: any) {
    this.queryParams.pageNumber = e;
    this.loadReservations(this.queryParams);
  }

  displayTime = (time: Date): string => {
    const splitTime = time.toString().split(":");
    return `${ splitTime[0] }:${ splitTime[1] }`;
  }

  popupFormInitialized = (e: { component: dxForm }) => this.editPopupForm = e.component;
  dataGridInitialized = (e: { component: dxDataGrid }) => this.dxDataGrid = e.component;
}
