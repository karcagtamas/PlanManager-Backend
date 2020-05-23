import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

/**
 * Loader Service for managing loading animations
 * @export
 * @class LoaderService
 */
@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  isLoading = new Subject<boolean>();

  /**
   * Creates an instance of LoaderService.
   * @memberof LoaderService
   */
  constructor() {}

  /**
   * Show loading animation
   * @memberof LoaderService
   */
  public show() {
    this.isLoading.next(true);
  }

  /**
   * Hide loading animation
   * @memberof LoaderService
   */
  public hide() {
    this.isLoading.next(false);
  }
}
