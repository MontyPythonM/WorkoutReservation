import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/common/base.component';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
   }

  ngOnInit(): void {
  }

}
