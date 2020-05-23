import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { ISideMenuItem } from 'src/app/interfaces/ISideMenuItem';

/**
 * Side Navigator Componenet
 * @export
 * @class SidenavComponent
 * @implements {OnInit}
 * @implements {OnChanges}
 */
@Component({
  selector: 'ks-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit, OnChanges {
  @Input() sideMenuItems: ISideMenuItem[];

  /**
   * Creates an instance of SidenavComponent.
   * @memberof SidenavComponent
   */
  constructor() {}

  /**
   * On Init hook
   * @memberof SidenavComponent
   */
  ngOnInit() {}

  /**
   * On Changes hook
   * @memberof SidenavComponent
   */
  ngOnChanges() {}
}
