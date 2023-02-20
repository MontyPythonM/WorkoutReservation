import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../common/base.component";

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.css']
})
export class ReservationDetailsComponent extends BaseComponent implements OnInit {

  ngOnInit(): void {
  }
}
