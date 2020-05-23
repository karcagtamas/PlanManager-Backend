import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserProfileDTO } from 'src/app/models';
import { UserService, CommonService } from 'src/app/services';
import { PageService } from 'src/app/services/page.service';
import { ActivatedRoute } from '@angular/router';

/**
 * Profile Component - Profile pages
 *
 * @export
 * @class ProfileComponent
 * @implements {OnInit}
 * @implements {OnDestroy}
 */
@Component({
  selector: 'ks-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit, OnDestroy {
  title = '';

  /**
   * Creates an instance of ProfileComponent.
   * @param {PageService} pageService
   * @param {CommonService} commonService
   * @param {ActivatedRoute} route
   * @memberof ProfileComponent
   */
  constructor(
    private pageService: PageService,
    private commonService: CommonService,
    private route: ActivatedRoute
  ) {}

  /**
   * On Init hook
   * Loads profile pages for side navigator
   * Subscribe on child's datas
   * @memberof ProfileComponent
   */
  ngOnInit() {
    this.loadProfilePages();
    this.route.url.subscribe(() => {
      this.title = this.route.snapshot.firstChild.data.title;
    });
  }

  /**
   * On Desctroy hook
   * Reset profile pages
   * @memberof ProfileComponent
   */
  ngOnDestroy() {
    this.resetProfilePages();
  }

  /**
   *  Loads profile pages for side navigator
   * @private
   * @memberof ProfileComponent
   */
  private loadProfilePages(): void {
    this.pageService.getProfilePages().then(res => {
      this.commonService.sideMenuItems = res;
    });
  }

  /**
   * Resets profile pages
   * @private
   * @memberof ProfileComponent
   */
  private resetProfilePages(): void {
    this.commonService.sideMenuItems = [];
  }
}
