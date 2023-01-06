import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {Router} from "@angular/router";
import {PageUrls} from "../../../environments/page-urls";

@Component({
  selector: 'app-administration',
  template: `
    <dx-tab-panel
      [dataSource]="tabs"
      [animationEnabled]="true"
      [swipeEnabled]="true"
      [selectedIndex]="setCurrentTab()"
      itemTemplate="tabContents"
      itemTitleTemplate="tabTitles"
      (onTitleClick)="selectTab($event)"
    >
      <div *dxTemplate="let tab of 'tabTitles'">
        <p class="tab-title">{{ tab.title }}</p>
      </div>
      <div *dxTemplate="let tab of 'tabContents'">
        <router-outlet></router-outlet>
      </div>
    </dx-tab-panel>
  `,
  styles: ['.tab-title { font-weight: 500 }']
})
export class AdministrationComponent extends BaseComponent {
  tabs: { title: string, url: string, icon: string }[];

  constructor(private router: Router) {
    super();
    this.tabs = [
      { title: "Users", url: PageUrls.administration.users, icon: "check" },
      { title: "Repetitive workouts", url: PageUrls.administration.repetitiveWorkouts, icon: "check" },
      { title: "Workout type tags", url: PageUrls.administration.workoutTypeTags, icon: "check" },
    ];
   }

  selectTab = (e: any) => this.router.navigate([this.tabs[e.itemIndex].url]);
  setCurrentTab = (): number => {
    return this.tabs.findIndex(tab => tab.url === this.router.url);
  }
}
