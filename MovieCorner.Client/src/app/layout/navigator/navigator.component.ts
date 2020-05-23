import { Component, OnInit } from '@angular/core';
import { CommonService, UserService } from 'src/app/services';
import { Router } from '@angular/router';
import { UserProfileDTO } from 'src/app/models';

/**
 * Navigator Componenet
 * @export
 * @class NavigatorComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.scss']
})
export class NavigatorComponent implements OnInit {
  user: UserProfileDTO;
  sideNavOpen = false;
  countOfSideMenuItems = 0;
  isMobileView = window.innerWidth < 600;

  /**
   * Creates an instance of NavigatorComponent.
   * @param {CommonService} commonService
   * @param {Router} router
   * @param {UserService} userService
   * @memberof NavigatorComponent
   */
  constructor(
    private commonService: CommonService,
    private router: Router,
    private userService: UserService
  ) {}

  /**
   * On Init hook
   * Determines side nav status and subscribes on the changes
   * Gets user profile datas
   * @memberof NavigatorComponent
   */
  ngOnInit() {
    this.sideNavOpen = this.commonService.sideNavIsOpen;
    this.commonService.state$.subscribe(res => {
      // If the items length has been changed
      if (this.countOfSideMenuItems !== res.sideMenuItems.length) {
        this.countOfSideMenuItems = res.sideMenuItems.length;

        // If the new value is zero then close the side navigator
        if (this.countOfSideMenuItems === 0) {
          this.sideNavOpen = false;
          this.commonService.sideNavIsOpen = false;
        }
      }
    });
    this.getUserProfile();
  }

  /**
   * Logout process
   * Clear state and local storage
   * @memberof NavigatorComponent
   */
  public logout(): void {
    this.commonService.token = '';
    this.commonService.userId = null;
    this.commonService.isLoggedIn = false;
    localStorage.removeItem('token');
    this.router.navigateByUrl('/oacts');
  }

  /**
   * Gets current user's profile
   * @private
   * @memberof NavigatorComponent
   */
  private getUserProfile(): void {
    this.userService
      .getUser()
      .then(res => {
        this.user = res;
      })
      .catch(() => {
        this.user = null;
      });
  }

  /**
   * Changes side navigator open status
   * @memberof NavigatorComponent
   */
  public changeSidenavStatus(): void {
    this.sideNavOpen = !this.sideNavOpen;
    this.commonService.sideNavIsOpen = this.sideNavOpen;
  }
}
