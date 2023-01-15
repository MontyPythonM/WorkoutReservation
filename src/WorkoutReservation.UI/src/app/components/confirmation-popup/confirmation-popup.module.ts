import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ConfirmationPopupComponent} from "./confirmation-popup.component";
import {DxButtonModule, DxPopupModule} from "devextreme-angular";


@NgModule({
  imports: [
    CommonModule,
    DxPopupModule,
    DxButtonModule
  ],
  declarations: [ConfirmationPopupComponent],
  exports: [ConfirmationPopupComponent],
})
export class ConfirmationPopupModule { }
