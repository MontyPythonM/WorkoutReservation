import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";
import {Reservation} from "../../../models/reservation.model";
import {ReservationService} from "../../../services/reservation.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.css']
})
export class ReservationDetailsComponent extends BaseComponent implements OnInit {
  reservation?: Reservation;

  constructor(private reservationService: ReservationService, private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
    this.loadReservation();
  }

  protected loadReservation() {
    this.subscribe(this.reservationService.getOwnReservationDetails(this.route.snapshot.params['id']), {
      next: (result: Reservation) => this.reservation = result
    });
  }
}
