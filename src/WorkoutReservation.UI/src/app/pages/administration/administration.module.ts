import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DxButtonModule, DxDataGridModule, DxTabPanelModule} from "devextreme-angular";
import {UsersComponent} from "./users/users.component";
import {WorkoutTypeTagsComponent} from "./workout-type-tags/workout-type-tags.component";
import {RepetitiveWorkoutsComponent} from "./repetitive-workouts/repetitive-workouts.component";
import {AdministrationComponent} from "./administration.component";
import {RouterModule} from "@angular/router";
import {ConfirmationPopupModule} from "../../components/confirmation-popup/confirmation-popup.module";

@NgModule({
  declarations: [
    AdministrationComponent,
    UsersComponent,
    WorkoutTypeTagsComponent,
    RepetitiveWorkoutsComponent
  ],
  imports: [
    CommonModule,
    DxTabPanelModule,
    DxButtonModule,
    DxDataGridModule,
    DxButtonModule,
    ConfirmationPopupModule,
    RouterModule.forChild([
      {
        path: '',
        component: AdministrationComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            redirectTo: 'users'
          },
          {
            path: 'users',
            component: UsersComponent,
          },
          {
            path: 'repetitive-workouts',
            component: RepetitiveWorkoutsComponent,
          },
          {
            path: 'workout-type-tags',
            component: WorkoutTypeTagsComponent,
          }
        ]
      }
    ])
  ]
})
export class AdministrationModule { }
