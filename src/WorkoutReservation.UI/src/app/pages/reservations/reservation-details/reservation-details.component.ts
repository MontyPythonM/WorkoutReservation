import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {Reservation} from "../../../models/reservation.model";
import {ReservationService} from "../../../services/reservation.service";
import {ActivatedRoute, Router} from "@angular/router";
import {pageUrls} from "../../../../environments/page-urls";
import {workoutIntensities} from "../../../models/enums/workout-intensity.enum";
import {ReservationStatus, reservationStatuses} from "../../../models/enums/reservation-status.enum";
import {DATE_FORMAT} from "../../../common/constants";
import {Permission} from 'src/app/models/enums/permission.enum';
import {EditReservation} from "../../../models/edit-reservation.model";
import dxForm from "devextreme/ui/form";

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.css']
})
export class ReservationDetailsComponent extends BaseComponent implements OnInit {
  reservation?: Reservation;
  intensities = workoutIntensities;
  statuses = reservationStatuses;
  routeId: number;
  dateFormat = DATE_FORMAT;
  isNotReserved: boolean;
  permissions = Permission;
  editPopupVisible: boolean;
  reservationCommand?: EditReservation;
  isCancelPopupVisible: boolean;
  private editPopupForm?: dxForm;

  constructor(private reservationService: ReservationService,
              private route: ActivatedRoute,
              private router: Router) {
    super();
    this.routeId = this.route.snapshot.params['id'];
    this.isNotReserved = false;
    this.editPopupVisible = false;
    this.isCancelPopupVisible = false;
  }

  ngOnInit(): void {
    this.loadReservation();
  }

  protected loadReservation() {
    this.subscribe(this.reservationService.getDetails(this.routeId), {
      next: (result: Reservation) => {
        this.reservation = result;
        this.isNotReserved = result.reservationStatus != ReservationStatus.Reserved;
      }
    });
  }

  cancelReservation() {
    this.subscribe(this.reservationService.cancelReservation(this.routeId), {
      next: () => {
        this.ngOnInit();
        this.notificationService.show("The reservation has been cancelled", "success")
      },
      error: () => this.notificationService.show("The reservation has not been canceled", "error")
    })
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

  openEditPopup = () => {
    this.reservationCommand = new EditReservation(this.reservation?.id!, this.reservation?.reservationStatus!, this.reservation?.note!);
    this.editPopupVisible = true;
  }

  closeEditPopup = () => this.editPopupVisible = false;

  openCancelPopup = () => this.isCancelPopupVisible = true;

  backToReservations = () => this.router.navigateByUrl(pageUrls.reservations);

  navigateToInstructor = (id: number) => this.router.navigateByUrl(pageUrls.instructors + '/' + id)
  navigateToWorkoutType = () => this.router.navigateByUrl(pageUrls.workoutTypes);

  displayStatus = () => this.statuses.find(x => x.index === this.reservation?.reservationStatus)!.value;
  displayIntensity = () => this.intensities.find(x => x.index === this.reservation?.intensity)!.value;

  displayTime = (time: Date): string => {
    const splitTime = time.toString().split(":");
    return `${ splitTime[0] }:${ splitTime[1] }`;
  }

  editPopupFormInit = (e: { component: dxForm }) => this.editPopupForm = e.component;
}
