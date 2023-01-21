import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PagerComponent} from "./pager.component";
import {DxButtonModule, DxNumberBoxModule} from "devextreme-angular";


@NgModule({
  imports: [
    CommonModule,
    DxButtonModule,
    DxNumberBoxModule
  ],
  declarations: [PagerComponent],
  exports: [PagerComponent],
})
export class PagerModule { }
