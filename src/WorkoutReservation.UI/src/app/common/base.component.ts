import {Component, OnDestroy} from "@angular/core";
import {catchError, Observable, PartialObserver, Subject, Subscription, takeUntil, throwError} from "rxjs";
import {NotificationService} from "../services/notification.service";
import {PermissionService} from "../services/identity/permission.service";
import {AppInjector} from "../app.module";

@Component({
  template: ""
})
export abstract class BaseComponent implements OnDestroy {

  private ngUnsubscribe: Subject<void>;
  protected permissionService = AppInjector.get(PermissionService) as PermissionService;
  protected notificationService = AppInjector.get(NotificationService) as NotificationService;

  constructor() {
    this.ngUnsubscribe = new Subject<void>();
  }

  subscribe<T>(observable: Observable<T>, observer?: PartialObserver<T>): Subscription {
    return observable.pipe(catchError(error => {
        switch (error.status) {
          case 403:
            this.notificationService.show('Action forbidden', 'error');
            break;
          default:
            this.notificationService.show('Error occurred', 'error');
        }
        return throwError(() => error);
      }),
      takeUntil(this.ngUnsubscribe)).subscribe(observer);
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  hasPermission = (permissions: string | Array<string>) => this.permissionService.hasPermissions(permissions);

  isAuthenticated = () => this.permissionService.hasValidToken();
}
