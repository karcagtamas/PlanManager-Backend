import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router
} from '@angular/router';
import { Observable } from 'rxjs';
import { CommonService } from '../services';

/**
 * Auth Guard for Authentication
 * @export
 * @class AuthGuard
 * @implements {CanActivate}
 */
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  /**
   * Creates an instance of AuthGuard.
   * @param {CommonService} commonService
   * @param {Router} router
   * @memberof AuthGuard
   */
  constructor(private commonService: CommonService, private router: Router) {}

  /**
   * If site has token grant access, else option require login
   * @param {ActivatedRouteSnapshot} next
   * @param {RouterStateSnapshot} state
   * @returns {(Observable<boolean | UrlTree>
   *     | Promise<boolean | UrlTree>
   *     | boolean
   *     | UrlTree)}
   * @memberof AuthGuard
   */
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (this.commonService.isLoggedIn) {
      return true;
    } else {
      this.router.navigateByUrl('oacts');
      return false;
    }
  }
}
