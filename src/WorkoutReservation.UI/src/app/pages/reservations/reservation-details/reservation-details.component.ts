import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {Reservation} from "../../../models/reservation.model";
import {ReservationService} from "../../../services/reservation.service";
import {ActivatedRoute, Router} from "@angular/router";
import {pageUrls} from "../../../../environments/page-urls";
import {EnumObject, enumToObjects} from "../../../models/enums/enum-converter";
import {WorkoutIntensity} from "../../../models/enums/workout-intensity.enum";
import {ReservationStatus} from "../../../models/enums/reservation-status.enum";
import {DATE_FORMAT} from "../../../common/constants";
import { Permission } from 'src/app/models/enums/permission.enum';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.css']
})
export class ReservationDetailsComponent extends BaseComponent implements OnInit {
  reservation?: Reservation;
  intensity: EnumObject[];
  status: EnumObject[];
  routeId: number;
  dateFormat = DATE_FORMAT;
  isNotReserved: boolean;
  permissions = Permission;

  constructor(private reservationService: ReservationService,
              private route: ActivatedRoute,
              private router: Router) {
    super();
    this.intensity = enumToObjects(WorkoutIntensity);
    this.status = enumToObjects(ReservationStatus);
    this.routeId = this.route.snapshot.params['id'];
    this.isNotReserved = false;
  }

  ngOnInit(): void {
    this.loadReservation();
  }

  protected loadReservation() {
    this.subscribe(this.reservationService.getOwnReservationDetails(this.routeId), {
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

  backToReservations = () => this.router.navigateByUrl(pageUrls.reservations);

  navigateToInstructor = (id: number) => this.router.navigateByUrl(pageUrls.instructors + '/' + id)
  navigateToWorkoutType = () => this.router.navigateByUrl(pageUrls.workoutTypes);

  displayStatus = () => this.status.find(x => x.index === this.reservation?.reservationStatus)!.value;
  displayIntensity = () => this.intensity.find(x => x.index === this.reservation?.intensity)!.value;

  displayTime = (time: Date): string => {
    const splitTime = time.toString().split(":");
    return `${ splitTime[0] }:${ splitTime[1] }`;
  }
}
