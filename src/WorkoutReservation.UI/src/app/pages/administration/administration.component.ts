import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {Router} from "@angular/router";
import {pageUrls} from 'src/environments/page-urls';
import {environment} from "../../../environments/environment";
import {Permission} from "../../models/enums/permission.enum";

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css']
})
export class AdministrationComponent extends BaseComponent {
  tabs: { title: string, url: string, icon: string }[];
  permissions = Permission;

  constructor(private router: Router) {
    super();
    this.tabs = [
      { title: "Users registry", url: pageUrls.administration.users, icon: "dx-icon-group" },
      { title: "Reservations", url: pageUrls.administration.reservations, icon: "dx-icon-event" },
      { title: "Repetitive workouts", url: pageUrls.administration.repetitiveWorkouts, icon: "dx-icon-tableproperties" },
      { title: "Workout type tags", url: pageUrls.administration.workoutTypeTags, icon: "dx-icon-tags" },
    ];
   }

  selectTab = (e: any) => this.router.navigate([this.tabs[e.itemIndex].url]);

  setCurrentTab = (): number => {
    return this.tabs.findIndex(tab => tab.url === this.router.url);
  }

  openHangfireDashboard() {
    return window.open(environment.serverUrl + pageUrls.hangfire, "Hangfire Dashboard");
  }
}
