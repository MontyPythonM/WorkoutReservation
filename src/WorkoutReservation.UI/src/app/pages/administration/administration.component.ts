import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {Router} from "@angular/router";
import {PageUrls} from "../../../environments/page-urls";

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css']
})
export class AdministrationComponent extends BaseComponent {
  tabs: {title: string, url: string}[];

  constructor(private router: Router) {
    super();
    this.tabs = [
      { title: "Users", url: PageUrls.administration.users },
      { title: "Repetitive workouts", url: PageUrls.administration.repetitiveWorkouts },
      { title: "Workout type tags", url: PageUrls.administration.workoutTypeTags },
    ];
   }

  selectTab = (e: any) => this.router.navigate([this.tabs[e.itemIndex].url]);
  setCurrentTab = (): number => this.tabs.findIndex(tab => tab.url === this.router.url);
}
