import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DxButtonModule, DxSelectBoxModule, DxTextBoxModule} from "devextreme-angular";
import {SearchPanelComponent} from "./search-panel.component";


@NgModule({
  imports: [
    CommonModule,
    DxButtonModule,
    DxTextBoxModule,
    DxSelectBoxModule
  ],
  declarations: [SearchPanelComponent],
  exports: [SearchPanelComponent],
})
export class SearchPanelModule { }
