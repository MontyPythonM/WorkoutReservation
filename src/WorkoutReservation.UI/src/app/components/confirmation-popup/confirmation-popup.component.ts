import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-confirmation-popup',
  template: `
    <dx-popup
      [(visible)]="visible"
      [showTitle]="true"
      [dragEnabled]="false"
      [showCloseButton]="false"
      [width]="500"
      [height]="200"
      titleTemplate="popupHeader">
      <div *dxTemplate="let header of 'popupHeader'">{{ title }}</div>
      <span class="content">{{ content }}</span>
      <dxi-toolbar-item location="after" toolbar="bottom">
        <dx-button
          class="popup-button"
          text="Cancel"
          type="normal"
          (onClick)="closePopup()"></dx-button>
        <dx-button
          text="Confirm"
          type="default"
          (onClick)="confirmPopup()"></dx-button>
      </dxi-toolbar-item>
    </dx-popup>
  `,
  styleUrls: ['./confirmation-popup.component.css']
})
export class ConfirmationPopupComponent {
  @Input() visible: boolean = false;
  @Input() content: string = "";
  @Input() title: string = "";
  @Output() confirm = new EventEmitter();
  @Output() visibleChange = new EventEmitter<boolean>();

  closePopup = () => {
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }

  confirmPopup = () => {
    this.confirm.emit();
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }
}
