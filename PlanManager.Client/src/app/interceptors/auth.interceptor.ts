import { Router } from '@angular/router';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { StateService } from '../services';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';

/**
 * Auth interceptor for token bearing
 * @export
 * @class AuthInterceptor
 * @implements {HttpInterceptor}
 */
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  /**
   * Creates an instance of AuthInterceptor.
   * @param {Router} router
   * @param {CommonService} stateService
   * @memberof AuthInterceptor
   */
  constructor(private router: Router, private stateService: StateService) {}

  /**
   * Intercept function
   * Link token to all request, if the backend gave back 401, then remove the token and redirect to the Login Page
   * @param {HttpRequest<any>} req
   * @param {HttpHandler} next
   * @returns {Observable<HttpEvent<any>>}
   * @memberof AuthInterceptor
   */
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.stateService.token) {
      const reqClone = req.clone({
        headers: req.headers.set(
          'Authorization',
          `Bearer ${this.stateService.token}`
        )
      });
      return next.handle(reqClone).pipe(
        tap(
          success => {},
          err => {
            if (err.status === 401 || err.status === 0) {
              this.stateService.token = '';
              this.stateService.userId = null;
              this.stateService.isLoggedIn = false;
              localStorage.removeItem('token');
              this.router.navigateByUrl('/oacts/login');
            }
          }
        )
      );
    } else {
      return next.handle(req.clone());
    }
  }
}
