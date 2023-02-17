import { Component } from '@angular/core';

@Component({
  selector: 'app-logo',
  template: `
    <div class="logo">
      <span class="workout">WORKOUT</span>
      <span class="reservation">RESERVATION</span>
    </div>
  `,
  styleUrls: ['./logo.component.css']
})
export class LogoComponent{
}
