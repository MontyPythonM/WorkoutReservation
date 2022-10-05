import { Component, OnDestroy } from "@angular/core";
import { Observable, PartialObserver, Subject, Subscription, takeUntil } from "rxjs";

@Component({
  template: ""
})
export abstract class BaseComponent implements OnDestroy {

  private ngUnsubscribe: Subject<void>;

  constructor() {
    this.ngUnsubscribe = new Subject<void>();
  }

  subscribe<T>(observable: Observable<T>, observer?: PartialObserver<T>): Subscription {
    return observable.pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(observer);
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}