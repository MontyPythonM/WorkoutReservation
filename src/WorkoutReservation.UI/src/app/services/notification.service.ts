import { Injectable } from '@angular/core';
import notify from 'devextreme/ui/notify';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  show(message: string, type: string): void {
    notify({
      message: message,
      position: 'top',
      type: type,
      minWidth: 150,
      maxWidth: 300,
      displayTime: 3500,
      animation: {
        show: { type: 'fade', duration: 400, from: 0, to: 1 },
        hide: { type: 'fade', duration: 40, to: 0 }
      }
    });
  }
}
