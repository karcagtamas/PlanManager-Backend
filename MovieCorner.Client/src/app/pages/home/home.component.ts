import {
  Component,
  OnInit,
  ChangeDetectorRef,
  OnDestroy,
  ViewChild
} from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';
import { CommonService } from 'src/app/services';
import { ISideMenuItem } from 'src/app/interfaces/ISideMenuItem';

/**
 * Home Component - Hom page for the most sub modules
 * @export
 * @class HomeComponent
 * @implements {OnInit}
 * @implements {OnDestroy}
 */
@Component({
  selector: 'ks-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
  mobileQuery: MediaQueryList;
  sideMenuItems: ISideMenuItem[] = [];
  sideNavOpen = false;

  @ViewChild('sidenav', { static: false }) sidenav: MatSidenav;

  /**
   * Creates an instance of HomeComponent.
   * Mobile query settings
   * @param {ChangeDetectorRef} changeDetectorRef
   * @param {MediaMatcher} media
   * @param {CommonService} commonService
   * @memberof HomeComponent
   */
  constructor(
    changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    private commonService: CommonService
  ) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    // tslint:disable-next-line: deprecation
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  /**
   * On Init hook
   * Check sidenav status and subscribe on the changes
   * @memberof HomeComponent
   */
  ngOnInit() {
    this.sideNavOpen = this.commonService.sideNavIsOpen;
    this.commonService.state$.subscribe(res => {
      if (res.sideNavIsOpen !== this.sideNavOpen) {
        // Set side nav status
        this.sideNavOpen = res.sideNavIsOpen;
        this.openOrCloseSidenav();
      }
      // Set side nav items
      this.sideMenuItems = res.sideMenuItems;
    });
  }

  /**
   * Opens or closes the side navigator
   * @memberof HomeComponent
   */
  public openOrCloseSidenav(): void {
    if (this.sideNavOpen && !this.sidenav.opened) {
      this.sidenav.open();
    }
    if (!this.sideNavOpen && this.sidenav.opened) {
      this.sidenav.close();
    }
  }

  /**
   * On Destroy hook
   * Removes listener on mobile query
   *
   * @memberof HomeComponent
   */
  ngOnDestroy(): void {
    // tslint:disable-next-line: deprecation
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  // tslint:disable-next-line: variable-name member-ordering
  private _mobileQueryListener: () => void;
}
