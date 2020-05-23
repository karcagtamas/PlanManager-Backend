import { Component, OnInit } from '@angular/core';

/**
 * User Messages Component
 * Display message between the current user and hist friends
 * @export
 * @class UserMessagesComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.scss']
})
export class UserMessagesComponent implements OnInit {
  /**
   * Creates an instance of UserMessagesComponent.
   * @memberof UserMessagesComponent
   */
  constructor() {}

  /**
   * On Init hook
   * @memberof UserMessagesComponent
   */
  ngOnInit() {}
}
