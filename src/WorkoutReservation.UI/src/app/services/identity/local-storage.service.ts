import {Injectable} from '@angular/core';
import {JWT_STORAGE_KEY} from "../../common/constants";

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  get = (): string | null  =>
    localStorage.getItem(JWT_STORAGE_KEY);

  set = (value: string): void =>
    localStorage.setItem(JWT_STORAGE_KEY, value);

  remove = (): void =>
    localStorage.removeItem(JWT_STORAGE_KEY);
}
