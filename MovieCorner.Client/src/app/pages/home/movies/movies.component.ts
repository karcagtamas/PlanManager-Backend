import { Component, OnInit, OnDestroy } from '@angular/core';
import { PageService } from 'src/app/services/page.service';
import { CommonService } from 'src/app/services';
import { ActivatedRoute } from '@angular/router';

/**
 * Movies Component - Movie pages
 *
 * @export
 * @class MoviesComponent
 * @implements {OnInit}
 * @implements {OnDestroy}
 */
@Component({
  selector: 'ks-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit, OnDestroy {
  title = '';

  /**
   * Creates an instance of MoviesComponent.
   * @param {PageService} pageService
   * @param {CommonService} commonService
   * @param {ActivatedRoute} route
   * @memberof MoviesComponent
   */
  constructor(
    private pageService: PageService,
    private commonService: CommonService,
    private route: ActivatedRoute
  ) {}

  /**
   * On Init hook
   * Load side menu pages
   * Subscribe on the child's datas
   * @memberof MoviesComponent
   */
  ngOnInit() {
    this.loadMoviesPages();
    this.route.url.subscribe(() => {
      this.title = this.route.snapshot.firstChild.data.title;
    });
  }

  /**
   * On Desctory hook
   * Reset movies
   * @memberof MoviesComponent
   */
  ngOnDestroy() {
    this.resetMoviesPages();
  }

  /**
   * Loads movie pages for side navigator
   * @private
   * @memberof MoviesComponent
   */
  private loadMoviesPages(): void {
    this.pageService.getMoviesPages().then(res => {
      this.commonService.sideMenuItems = res;
    });
  }

  /**
   * Resets movies
   * @private
   * @memberof MoviesComponent
   */
  private resetMoviesPages(): void {
    this.commonService.sideMenuItems = [];
  }
}
