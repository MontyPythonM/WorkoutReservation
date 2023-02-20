import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {
  DxButtonModule,
  DxDataGridModule,
  DxFormModule,
  DxLoadPanelModule,
  DxPopupModule,
  DxTabPanelModule
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
import {UserReservationsComponent} from './user-reservations/user-reservations.component';

@NgModule({
  declarations: [
    AdministrationComponent,
    UsersComponent,
    WorkoutTypeTagsComponent,
    RepetitiveWorkoutsComponent,
    UserReservationsComponent
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
    ConfirmationPopupModule,
    PagerModule,
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
            path: 'user-reservations',
            component: UserReservationsComponent
          }
        ]
      }
    ])
  ]
})
export class AdministrationModule { }
