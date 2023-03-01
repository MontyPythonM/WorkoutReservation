import {Component} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {Router} from "@angular/router";
import {pageUrls} from 'src/environments/page-urls';

@Component({
  selector: 'app-administration',
  template: `
    <dx-tab-panel
      id="tab-panel"
      [dataSource]="tabs"
      [animationEnabled]="true"
      [selectedIndex]="setCurrentTab()"
      itemTemplate="tabContents"
      itemTitleTemplate="tabTitles"
      (onTitleClick)="selectTab($event)"
    >
      <div *dxTemplate="let tab of 'tabTitles'">
        <p>
          <span class="{{ tab.icon }} dx-icon-custom-style"></span>
          <span class="tab-title">{{ tab.title }}</span>
        </p>
      </div>
      <div *dxTemplate="let tab of 'tabContents'">
        <router-outlet></router-outlet>
      </div>
    </dx-tab-panel>
  `,
  styles: [`
    .tab-title { font-weight: 500; font-size: 15px; }
    .dx-icon-custom-style { font-size: 15px; margin-right: 8px; }
    #tab-panel { background-color: white }
  `]
})
export class AdministrationComponent extends BaseComponent {
  tabs: { title: string, url: string, icon: string }[];

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
}
