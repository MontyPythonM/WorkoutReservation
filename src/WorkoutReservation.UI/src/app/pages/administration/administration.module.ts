import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {
  DxButtonModule,
  DxDataGridModule,
  DxFormModule,
  DxLoadPanelModule,
  DxPopupModule,
  DxTabPanelModule,
  DxTagBoxModule
} from "devextreme-angular";
import {UsersComponent} from "./users/users.component";
import {WorkoutTypeTagsComponent} from "./workout-type-tags/workout-type-tags.component";
import {RepetitiveWorkoutsComponent} from "./repetitive-workouts/repetitive-workouts.component";
import {AdministrationComponent} from "./administration.component";
import {RouterModule} from "@angular/router";
import {ConfirmationPopupModule} from "../../components/confirmation-popup/confirmation-popup.module";
import {PagerModule} from "../../components/pager/pager.module";
import {Permission} from "../../models/enums/permission.enum";
import {AuthGuardService} from "../../services/identity/auth-guard.service";
import {ReservationRegistryComponent} from './reservation-registry/reservation-registry.component';
import {SearchPanelModule} from "../../components/search-panel/search-panel.module";

@NgModule({
  declarations: [
    AdministrationComponent,
    UsersComponent,
    WorkoutTypeTagsComponent,
    RepetitiveWorkoutsComponent,
    ReservationRegistryComponent
  ],
  imports: [
    CommonModule,
    DxTabPanelModule,
    DxButtonModule,
    DxDataGridModule,
    DxButtonModule,
    DxPopupModule,
    DxFormModule,
    DxLoadPanelModule,
    DxTagBoxModule,
    ConfirmationPopupModule,
    PagerModule,
    SearchPanelModule,
    RouterModule.forChild([
      {
        path: '',
        canActivate: [AuthGuardService],
        data: {
          permission: Permission.OpenAdministrationPage
        },
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
          },
          {
            path: 'reservations',
            component: ReservationRegistryComponent
          }
        ]
      }
    ])
  ]
})
export class AdministrationModule { }
