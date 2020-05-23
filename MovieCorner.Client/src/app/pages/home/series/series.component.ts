import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonService, PageService } from 'src/app/services';

/**
 * Series Componenet - Series pages
 *
 * @export
 * @class SeriesComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-series',
  templateUrl: './series.component.html',
  styleUrls: ['./series.component.scss']
})
export class SeriesComponent implements OnInit, OnDestroy {
  /**
   * Creates an instance of SeriesComponent.
   * @param {PageService} pageService
   * @param {CommonService} commonService
   * @memberof SeriesComponent
   */
  constructor(
    private pageService: PageService,
    private commonService: CommonService
  ) {}

  /**
   * On Init hook
   * Loads series pages for side navigator
   * @memberof SeriesComponent
   */
  ngOnInit() {
    this.loadSeriesPages();
  }

  /**
   * On Destrey hook
   * Resets series pages
   * @memberof SeriesComponent
   */
  ngOnDestroy() {
    this.resetSeriesPages();
  }

  /**
   * Loads series pages for side navigator
   * @private
   * @memberof SeriesComponent
   */
  private loadSeriesPages(): void {
    this.pageService.getSeriesPages().then(res => {
      this.commonService.sideMenuItems = res;
    });
  }

  /**
   * Resets profile pages
   * @private
   * @memberof ProfileComponent
   */
  private resetSeriesPages(): void {
    this.commonService.sideMenuItems = [];
  }
}
