import { LoaderService } from './../services';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { finalize } from 'rxjs/operators';

/**
 * Loader interceptor for loader animation
 * @export
 * @class LoaderInterceptor
 * @implements {HttpInterceptor}
 */
@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  /**
   * Creates an instance of LoaderInterceptor.
   * @param {LoaderService} loaderService
   * @memberof LoaderInterceptor
   */
  constructor(public loaderService: LoaderService) {}

  /**
   * Intercept function
   * At start of the request, the loader animation appear
   * At end of the request, the loader animation disappear
   * @param {HttpRequest<any>} req
   * @param {HttpHandler} next
   * @returns {Observable<HttpEvent<any>>}
   * @memberof LoaderInterceptor
   */
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.loaderService.show();
    return next.handle(req).pipe(finalize(() => this.loaderService.hide()));
  }
}
