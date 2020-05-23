import { Component, OnInit, OnDestroy } from '@angular/core';
import { PageService, CommonService } from 'src/app/services';
import { ISideMenuItem } from 'src/app/interfaces';
import { ActivatedRoute } from '@angular/router';

/**
 * Todos Component - Todo pages
 *
 * @export
 * @class TodosComponent
 * @implements {OnInit}
 * @implements {OnDestroy}
 */
@Component({
  selector: 'ks-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss']
})
export class TodosComponent implements OnInit, OnDestroy {
  title = '';

  /**
   * Creates an instance of TodosComponent.
   * @param {PageService} pageService
   * @param {CommonService} commonService
   * @param {ActivatedRoute} route
   * @memberof TodosComponent
   */
  constructor(
    private pageService: PageService,
    private commonService: CommonService,
    private route: ActivatedRoute
  ) {}

  /**
   * On Init hook
   * Loads todo pages for side navigator
   * Subscribes on child's datas
   * @memberof TodosComponent
   */
  ngOnInit() {
    this.loadTodosPages();
    this.route.url.subscribe(() => {
      this.title = this.route.snapshot.firstChild.data.title;
    });
  }

  /**
   * On Destroy hook
   * Resets todo pages
   * @memberof TodosComponent
   */
  ngOnDestroy() {
    this.resetTodosPages();
  }

  /**
   * Loads todo pages for side navigator
   * @private
   * @memberof TodosComponent
   */
  private loadTodosPages(): void {
    this.pageService.getTodoPages().then(res => {
      this.commonService.sideMenuItems = res;
    });
  }

  /**
   * Resets todo pages
   * @private
   * @memberof TodosComponent
   */
  private resetTodosPages(): void {
    this.commonService.sideMenuItems = [];
  }
}
